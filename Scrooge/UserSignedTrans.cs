namespace Scrooge
{
    using Global;
    using System;

    [Serializable]
    public class UserSignedTrans : SignedMessage
    {
        [NonSerialized]
        private ITransfer transOrig;
        private Transfer transAutho;

        public Transfer Transfer
        {
            get { return transAutho; }
        }

        public UserSignedTrans(ITransfer transOrig, Transfer transAutho, Bytes userPk, Bytes userSgndTrans)
            :base(userPk, userSgndTrans)
        {
            this.transOrig = transOrig;
            this.transAutho = transAutho;
        }

        public bool isValidUserSignature()
        {
            var srlzTrans = new SerializedTransfer(this.transOrig);
            return base.IsValidSignedMsg(srlzTrans.SerializedObj, this.PublicKey);
        }

        private bool IsValidSignedMsg(SerializedTransfer srlzdTrans, Bytes publicKey)
        {
            return this.IsValidSignedMsg(srlzdTrans.SerializedObj, publicKey);
        }

        //public static implicit operator global::Global.UserSignedTrans(UserSignedTrans usrSgndTrans)
        //{
        //    return new global::Global.UserSignedTrans(usrSgndTrans.PublicKey, usrSgndTrans.SignedData);
        //}
    }
}
