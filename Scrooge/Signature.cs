namespace Scrooge
{
    using Global;

    public class Signature : global::Global.Signature
    {
        public Signature(KeySize keySize)
            :base(keySize)
        {
        }

        public UserSignedTrans SignMsg(Transfer trans)
        {
            var srlzdTrans = new SerializedTransfer(trans);
            return new UserSignedTrans(trans, trans, PublicKey, this.SignMsg(srlzdTrans.SerializedObj));
        }

        public AuthoritySignedTrans SignMsg(UserSignedTrans userSgndTrans)
        {
            return new AuthoritySignedTrans(userSgndTrans, PublicKey, this.SignMsg(userSgndTrans.SignedData));
        }
    }
}
