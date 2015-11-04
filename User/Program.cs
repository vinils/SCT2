//using Global;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace User
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var alice = new Signature(Signature.KeySize.s256);
//            var clark = new Signature(Signature.KeySize.s256);

//            // alice receive the trans
//            SerializedITransferHash received =  null;
//            var aliceLastHash = received.DeserializeTransfer();
//            var transfer = aliceLastHash.AuthoritySignedTrans.UserSignedTransfer.Transfer;
//            //check if it is a valid scrooge signature
//            if (!aliceLastHash.AuthoritySignedTrans.isValidAuthoritySignature(Global.Global.ScroogePk))
//                throw new Exception("this transaction was not signed by scrooge");
//            // alice add the transfer to her account
//            TransferId[] aliceDestyIds = new TransferId[10];
//            var count = 0;
//            foreach (var destiny in transfer.Destinies)
//            {
//                if (destiny.DestinyPk == alice.PublicKey)
//                {
//                    aliceDestyIds[count++] = destiny;
//                }
//            }

//            // alice want to transfer 60 to clark
//            var originIds = new TransferId[2];
//            double transValue = 60;
//            double valueAux = transValue;
//            for (var x = 0; valueAux <= 0; valueAux -= aliceDestyIds[x].Value.Value, x++)
//            {
//                originIds[x] = aliceDestyIds[x];
//            }
//            Origin origin = new Origin(aliceLastHash, originIds);
//            Origin[] origins = new Origin[] { origin };
//            var alDestiny = new TransferInfo(transValue, clark.PublicKey);
//            var alDestinies = new TransferInfo[] { alDestiny };

//            var trans1 = new Transfer(origins, alDestinies);
//            //alice sign the trans to prove the she is the owner
//            var userSgndTransBytes = alice.SignMsg(trans1);
//            var userSgndTrans = new UserSignedTrans(trans1, alice.PublicKey, userSgndTransBytes);

//            //alice send the transfer to scrooge
//            var sender = new SerializedUserSignedTrans(userSgndTrans);
//        }
//    }
//}
