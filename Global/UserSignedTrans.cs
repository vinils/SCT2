namespace Global
{
    using System;

    [Serializable]
    public class UserSignedTrans : SignedMessage
    {
        public UserSignedTrans(Bytes userPk, Bytes userSgndTrans)
            : base(userPk, userSgndTrans)
        {
        }

        public bool isValidUserSignature(Bytes ownerPk, ITransfer trans)
        {
            var srlzTrans = new SerializedTransfer(trans);
            return this.IsValidSignedMsg(srlzTrans.SerializedObj, ownerPk);
        }

        private bool IsValidSignedMsg(SerializedTransfer srlzdTrans, Bytes publicKey)
        {
            return this.IsValidSignedMsg(srlzdTrans.SerializedObj, publicKey);
        }
    }
}
