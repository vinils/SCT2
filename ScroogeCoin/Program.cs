using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            TransferHash scroogeLastHash;
            var scrooge = new Signature(256);
            Global.ScroogePk = scrooge.PublicKey;
            var alice = new Signature(256);
            var bob = new Signature(256);
            var clark = new Signature(256);
            var david = new Signature(256);

            //scrooge create first coin transfer
            var transInfo1 = new TransferInfo(50, alice.PublicKey);
            var transInfo2 = new TransferInfo(100, bob.PublicKey);
            var transInfo3 = new TransferInfo(50, alice.PublicKey);
            var transInfos = new TransferInfo[] { transInfo1, transInfo2, transInfo3 };

            var coin = new Transfer(new Coin(), transInfos);
            var serializedCoin = new SerializedTransfer(coin);
            var userSgndTrans = scrooge.SignTransfer(serializedCoin);
            var authoSgndTrans = scrooge.SignTransfer(userSgndTrans);
            scroogeLastHash = new TransferHash(authoSgndTrans);
            ITransferHash scroogeInterfaceLastHash = scroogeLastHash;
            var srlzLastHash = new SerializedITransferHash(scroogeInterfaceLastHash);



            //var origin = new Origin(scroogeLastHash, new TransferId[] { coin.DestiniesInfo[0], coin.DestiniesInfo[1] });
            //var destiniesTest = new TransferId[] {
            //    scroogeLast   Hash.AuthoritySignedTrans.UserSignedTransfer.SerializedTransfer.Transfer.DestiniesInfo[0],
            //    scroogeLastHash.AuthoritySignedTrans.UserSignedTransfer.SerializedTransfer.Transfer.DestiniesInfo[2]};

            //var origins = new Origin[10];
            //origins[0] = new Origin(scroogeLastHash, new TransferId[] { destiniesTest[0], destiniesTest[1] });

            // alice receive the trans
            ITransferHash aliceLastHash = srlzLastHash.DeserializeTransfer();
            //check if it is a valid scrooge signature
            if (!aliceLastHash.AuthoritySignedTrans.isValidAuthoritySignature(Global.ScroogePk))
                throw new Exception("this transaction was not signed by scrooge");
            // alice add the transfer to her account
            TransferId[] aliceDestyIds = new TransferId[10];
            var count = 0;
            foreach (var destiny in aliceLastHash.AuthoritySignedTrans.UserSignedTransfer.Transfer.Destinies)
            {
                if (destiny.DestinyPk == alice.PublicKey)
                {
                    aliceDestyIds[count++] = destiny;
                }
            }

            // alice want to transfer 60 to clark
            IOriginId[] originIds = new TransferId[2];
            double transValue = 60;
            double valueAux = transValue;
            for (var x = 0; valueAux <= 0; valueAux -= aliceDestyIds[x].Value.Value, x++)
            {
                originIds[x] = aliceDestyIds[x];
            }
            IOrigin origin = new Origin(aliceLastHash, originIds);
            IOrigin[] origins = new IOrigin[] { origin };
            var alDestiny = new TransferInfo(transValue, clark.PublicKey);
            var alDestinies = new TransferInfo[] { alDestiny };
            var alDestiniesMan = new DestinyIdManage(alDestinies);

            var trans1 = new Transfer(origins, alDestiniesMan);
            //alice sign the trans to prove the she is the owner
            var serializedTrans1 = new SerializedTransfer(trans1);
            var trans1UserSgnd = alice.SignTransfer(serializedTrans1);

            //scrooge receive the user signed trans and attach that to the chain
            //scrooge deserialize the trans
            var dsrlzdTrans = trans1UserSgnd.Transfer;
            var scroogeUserTrans = new Transfer(scroogeLastHash, trans1UserSgnd.SerializedTransfer.Transfer);
            var srlzdScroogeUserTrans = new SerializedTransfer(scroogeUserTrans);
            var scroogeUserSgndTrans = new UserSignedTrans(srlzdScroogeUserTrans, trans1UserSgnd.PublicKey, trans1UserSgnd.SignedData);
            //scrooge validate - is she the owner(validate her signdMessage) n is previous transaction the last transaction of the chain
            if (!scroogeUserSgndTrans.isValidUserSignature(scroogeLastHash.AuthoritySignedTrans.UserSignedTransfer.SerializedTransfer.Transfer.Destinies))
                throw new Exception("Invald signature");
            // scrooge sign the transaction
            var scroogeSgndTrans = scrooge.SignTransfer(scroogeUserSgndTrans);
            scroogeLastHash = scroogeSgndTrans.Hash;

            // bob receive the trans
            ITransferHash bobLastHash = scroogeLastHash;

            //bob receibe the transfer n validate if it is destinated to him
            if (bobLastHash.AuthoritySignedTrans.UserSignedTransfer.Transfer.Transfer.Destinies != bob.PublicKey)
                throw new Exception("this transaction dont belong to bob");
            //check if it is a valid scrooge signature
            if (!bobLastHash.AuthoritySignedTrans.isValidAuthoritySignature(Global.ScroogePk))
                throw new Exception("this transaction was not signed by scrooge");
            //bob transfer to clark
            var trans2 = new Transfer(bobLastHash, clark.PublicKey);
            //bob sign the trans to prove the he is the owner
            var serializedTrans2 = new SerializedTransfer(trans2);
            var trans2UserSgnd = bob.SignTransfer(serializedTrans2);

            //scrooge receive the user signed trans and attach that to the chain
            var scroogeUserTrans2 = new Transfer(scroogeLastHash, trans2UserSgnd.SerializedTransfer.Transfer);
            var srlzdScroogeUserTrans2 = new SerializedTransfer(scroogeUserTrans2);
            var scroogeUserSgndTrans2 = new UserSignedTrans(srlzdScroogeUserTrans2, trans2UserSgnd.PublicKey, trans2UserSgnd.SignedData);
            //scrooge validate - is she the owner(validate her signdMessage) n is previous transaction the last transaction of the chain
            if (!scroogeUserSgndTrans2.isValidUserSignature(scroogeLastHash.AuthoritySignedTrans.UserSignedTransfer.SerializedTransfer.Transfer.Destinies))
                throw new Exception("Invald signature");
            // scrooge sign the transaction
            var scroogeSgndTrans2 = scrooge.SignTransfer(scroogeUserSgndTrans2);
            scroogeLastHash = scroogeSgndTrans2.Hash;


            // bob receive the trans
            ITransferHash clarkLastHash = scroogeLastHash;



            //var trans2Hash = new TransferSignature(trans2, clark);
            //var trans3 = new Transfer(trans2Hash, david.PublicKey);

            //var b = trans3.Previous.isValidHash();






            //var scrooge = new Authority();
            //var alice = new Person();
            //var bob = new Person();
            //var clark = new Person();
            //var david = new Signature(256);

            //var coin = scrooge.CreateCoin(alice.PublicKey);

            //var trans1 = alice.PayTo(coin, bob.PublicKey);

            //var trans2 = bob.PayTo(coin, clark.PublicKey);

            //var trans3 = clark.PayTo(coin, david.PublicKey);

            //var b = trans3.Previous.isValidHash();
        }
    }

}
