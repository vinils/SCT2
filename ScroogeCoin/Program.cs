using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    using Global;
    class Program
    {
        static void Main(string[] args)
        {
            //            Scrooge.Program.Main(null);

            //Scrooge.OriginLinkedList2 nonExpendedOrigs = new Scrooge.OriginLinkedList2();
            var scrooge2 = new Scrooge.Person();
            //var scrooge = new Scrooge.Signature(Signature.KeySize.s256);
            //Global.ScroogePk = scrooge.PublicKey;
            var alice = new User.Person();
            var bob = new User.Signature(Signature.KeySize.s256);
            var clark = new User.Signature(Signature.KeySize.s256);
            var david = new User.Signature(Signature.KeySize.s256);

            //scrooge create first coin transfer
            var transInfo1 = new TransferInfo(50, alice.PublicKey);
            var transInfo2 = new TransferInfo(100, bob.PublicKey);
            var transInfo3 = new TransferInfo(40, alice.PublicKey);
            var transInfos = new TransferInfo[] { transInfo1, transInfo2, transInfo3 };
            var transIds = transInfos.ToTransIdsLinkedList();

            var approval = scrooge2.Temp(transIds);

            //var transCoin = new Scrooge.Transfer(new Scrooge.Coin(), transIds.ToArray());
            //var userSgndTrans1 = scrooge.SignMsg(transCoin);
            //var authoSgndTrans1 = scrooge.SignMsg(userSgndTrans1);
            //var transHash1 = authoSgndTrans1.Hash;

            ////save non spent ids
            //nonExpendedOrigs.Add(transHash1, transIds);

            ////send to alice
            //var srlzdLastHash = new SerializedTransferHash(transHash1);
            //var srlzdTrans = new SerializedTransfer(transCoin);
            //var transApproved = new AuthorityApproval(srlzdLastHash, srlzdTrans);

            var h = approval.SrlzdTransferHash.DeserializeTransfer();
            var t = approval.SrlzdTrans.DeserializeTransfer();

            alice.AddTransfer(approval);

            //// alice want to transfer 60 to clark
            var alDestiny = new TransferInfo(60, clark.PublicKey);
            var alDestinies = new TransferInfo[] { alDestiny };

            var aliceRequest = alice.PayTo(alDestinies);

            var t2 = aliceRequest.SrlzdTrans.DeserializeTransfer();
            var h2 = aliceRequest.SrlzdUsrSgndTrans.DeserializeTransfer();

            var scroogeAutho = scrooge2.AddTransfer(aliceRequest);

            // bob receive the hash
            var bobLastHash = scroogeAutho.SrlzdTransferHash.DeserializeTransfer();
            var bobtransfer = scroogeAutho.SrlzdTrans.DeserializeTransfer();

            //bob receibe the transfer n validate if it is destinated to him
            //if (bobLastHash.AuthoritySignedTrans.UserSignedTransfer.Transfer.Transfer.Destinies != bob.PublicKey)
            //    throw new Exception("this transaction dont belong to bob");
            ////check if it is a valid scrooge signature
            if (!bobLastHash.AuthoritySignedTrans.isValidAuthoritySignature(Global.ScroogePk))
                throw new Exception("this transaction was not signed by scrooge");
            ////bob transfer to clark
            //var trans2 = new Transfer(bobLastHash, clark.PublicKey);
            ////bob sign the trans to prove the he is the owner
            //var serializedTrans2 = new SerializedTransfer(trans2);
            //var trans2UserSgnd = bob.SignTransfer(serializedTrans2);

            ////scrooge receive the user signed trans and attach that to the chain
            //var scroogeUserTrans2 = new Transfer(scroogeLastHash, trans2UserSgnd.SerializedTransfer.Transfer);
            //var srlzdScroogeUserTrans2 = new SerializedTransfer(scroogeUserTrans2);
            //var scroogeUserSgndTrans2 = new UserSignedTrans(srlzdScroogeUserTrans2, trans2UserSgnd.PublicKey, trans2UserSgnd.SignedData);
            ////scrooge validate - is she the owner(validate her signdMessage) n is previous transaction the last transaction of the chain
            //if (!scroogeUserSgndTrans2.isValidUserSignature(scroogeLastHash.AuthoritySignedTrans.UserSignedTransfer.SerializedTransfer.Transfer.Destinies))
            //    throw new Exception("Invald signature");
            //// scrooge sign the transaction
            //var scroogeSgndTrans2 = scrooge.SignTransfer(scroogeUserSgndTrans2);
            //scroogeLastHash = scroogeSgndTrans2.Hash;


            //// bob receive the trans
            //ITransferHash clarkLastHash = scroogeLastHash;



            ////var trans2Hash = new TransferSignature(trans2, clark);
            ////var trans3 = new Transfer(trans2Hash, david.PublicKey);

            ////var b = trans3.Previous.isValidHash();






            ////var scrooge = new Authority();
            ////var alice = new Person();
            ////var bob = new Person();
            ////var clark = new Person();
            ////var david = new Signature(256);

            ////var coin = scrooge.CreateCoin(alice.PublicKey);

            ////var trans1 = alice.PayTo(coin, bob.PublicKey);

            ////var trans2 = bob.PayTo(coin, clark.PublicKey);

            ////var trans3 = clark.PayTo(coin, david.PublicKey);

            ////var b = trans3.Previous.isValidHash();
        }
    }

}
