namespace Scrooge
{
    using Global;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Person
    {
        private OriginLinkedList nonExpended = new OriginLinkedList();
        protected Signature mySignature = new Signature(Signature.KeySize.s256);

        public byte[] PublicKey
        {
            get { return mySignature.PublicKey; }
        }

        public Person()
        {
            Global.ScroogePk = mySignature.PublicKey;
        }

        public AuthorityApproval AddTransfer(UserRequest usrRequest)
        {
            var usrSgndTrans = usrRequest.SrlzdUsrSgndTrans.DeserializeTransfer();
            var usrTrans = usrRequest.SrlzdTrans.DeserializeTransfer();

            //scrooge find the pointer to the origins
            var linkedOrigns = usrTrans.Origins.ConvertToOrigin(nonExpended);

            if (!linkedOrigns.IsSameDestiny())
                throw new Exception("Invalid transfer with diferentes origins public keys");

            var trans = new Transfer(linkedOrigns.ToOriginArray(), usrTrans.TransferIds);
            var sgndTrans = new UserSignedTrans(usrTrans, trans, usrSgndTrans.PublicKey, usrSgndTrans.SignedData);

            //scrooge validate - is she the owner(validate her signdMessage) n is previous transaction the last transaction of the chain
            if (!sgndTrans.isValidUserSignature())
                throw new Exception("Invald signature");
            
            // scrooge sign the transaction
            var scroogeSgndTrans = mySignature.SignMsg(sgndTrans);

            //removing orign expend
            foreach (var orig in linkedOrigns)
            {
                var nonExpendedOrig = nonExpended.Find(orig.Hash.HashCode);

                foreach(var ids in orig.TransferIds)
                {
                    nonExpendedOrig.Remove(ids.Id);
                }

                if (nonExpendedOrig.TransferIds == null)
                    nonExpended.Remove(nonExpendedOrig.Hash);
            }

            //add destiny to non expended origins 
            //nonExpended += linkedOrigns;
            linkedOrigns.MoveTo(nonExpended);

            //send the transfer hash
            var srlzdLastHash = new SerializedTransferHash(scroogeSgndTrans.Hash);
            var srlzdTrans = new SerializedTransfer(trans);

            return new AuthorityApproval(srlzdLastHash, srlzdTrans);

        }

        //public OriginArray LinkTransfer(IOrigins usrOrigins)
        //{
        //    return usrOrigins.ConvertToOrigin(nonExpended);
        //}
        public AuthorityApproval Temp(TransferIdLinkedList transIds)
        {
            var transCoin = new Transfer(new Coin(), transIds.ToArray());
            var userSgndTrans1 = mySignature.SignMsg(transCoin);
            var authoSgndTrans1 = mySignature.SignMsg(userSgndTrans1);
            var transHash1 = authoSgndTrans1.Hash;

            //save non spent ids
            nonExpended.Add(transHash1, transIds);

            //send to alice
            var srlzdLastHash = new SerializedTransferHash(transHash1);
            var srlzdTrans = new SerializedTransfer(transCoin);
            return new AuthorityApproval(srlzdLastHash, srlzdTrans);

        }
    }
}