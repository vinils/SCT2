using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    using Global;

    public class Person
    {
        private OriginLinkedList wallet = new OriginLinkedList();
        protected Signature mySignature = new Signature(Signature.KeySize.s256);

        public byte[] PublicKey
        {
            get { return mySignature.PublicKey; }
        }

        public Person()
        {
        }

        public void AddTransfer(AuthorityApproval usrTrans)
        {
            var transHash = usrTrans.SrlzdTransferHash.DeserializeTransfer();
            var trans = usrTrans.SrlzdTrans.DeserializeTransfer();

            //check if it is a valid scrooge signature
            if (!transHash.AuthoritySignedTrans.isValidAuthoritySignature(Global.ScroogePk))
                throw new Exception("this transaction was not signed by scrooge");

            // alice add the transfer to her account

            TransferIdNode ids = null;

            #warning must b improved 
            foreach (var usrId in trans.TransferIds)
            {
                if (usrId.DestinyPk == mySignature.PublicKey)
                    ids = new TransferIdNode(ids, usrId.Value, usrId.DestinyPk, usrId.Id);
            }

            wallet.Add(transHash, ids);

            //wallet.Add(transHash, trans.TransferIds.ToLinkedList());
        }

        public UserRequest PayTo(TransferInfo[] infos)
        {
            var value = 0.0;
            OriginLinkedList newOrig = new OriginLinkedList();

            //alice select the unspend origin which fits to the value she want to expend
            foreach(var info in infos)
            {
                value += info.Value;

                foreach (var walOrig in wallet)
                {
                    TransferIdNode ids = null;

                    foreach (var walId in walOrig.TransferIds)
                    {
                        ids = new TransferIdNode(ids, walId.Value, walId.DestinyPk, walId.Id);

                        value -= walId.Value;
                        if (value <= 0)
                            break;
                    }

                    newOrig.Add(walOrig.Hash, ids);

                    if (value <= 0)
                        break;
                }
            }

            //send the difference back to alice
            TransferInfo[] newInfos;

            if (value < 0)
            {
                newInfos = new TransferInfo[infos.Length + 1];
                for (int x = 0; x <= infos.Length - 1; x++)
                    newInfos[x] = infos[x];

                newInfos[infos.Length] = new TransferInfo(value * -1, mySignature.PublicKey);
            }
            else
            {
                newInfos = infos;
            }

            //remove spend ids
            foreach (var wOrig in wallet)
                foreach (var nOrig in newOrig)
                    foreach (var nId in nOrig.TransferIds)
                        wOrig.Remove(nId.Id);

            ///////////// deve remover apenas as origs que estao nullas
            //remove spend origs
            foreach (var nOrig in newOrig)
                wallet.Remove(nOrig.Hash);

            ////converter o new origins para ser automaticamente cast
            var trans = new Transfer(newOrig.ToOriginArray(), newInfos);
            var transSgnd = mySignature.SignMsg(trans);

            var srlzdSgndTrans = new SerializedUserSignedTrans(transSgnd);
            var srlzdTrans = new SerializedTransfer(trans);

            return new UserRequest(srlzdSgndTrans, srlzdTrans);
        }

        //public virtual void CheckTransfers(TransferApproved trans)
        //{
        //    //if (!trans.isCreateCoin())
        //    //    if (trans.Info.PreviousTransSignedByMe.isSigner(mySignature.PublicKey))
        //    //        throw new Exception("This transfer doesnt belong to me");

        //    //trans.isScroogeSignature();
        //    //trans.isValidApprovedSignedMsg();
        //}
    }
}