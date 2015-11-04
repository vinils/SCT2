using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge
{
    using Global;

    [Serializable]
    public class AuthoritySignedTrans : SignedMessage, IAuthoritySignedTrans
    {
        private UserSignedTrans userSgndTrans;

        public UserSignedTrans UserSignedTransfer
        {
            get { return userSgndTrans; }
        }

        public TransferHash Hash
        {
            get { return new TransferHash(this); }
        }

        ITransferHash IAuthoritySignedTrans.Hash
        {
            get { return new TransferHash(this); }
        }

        public AuthoritySignedTrans(UserSignedTrans userSgndTrans, Bytes authorityPk, Bytes authoSgndTrans)
            : base(authorityPk, authoSgndTrans)
        {
            this.userSgndTrans = userSgndTrans;
        }

        public bool isValidAuthoritySignature(Bytes ownerPk)
        {
            return this.IsValidSignedMsg(this.userSgndTrans, ownerPk);
        }

        private bool IsValidSignedMsg(UserSignedTrans userSgndTrans, Bytes publicKey)
        {
            return this.IsValidSignedMsg(userSgndTrans.SignedData, publicKey);
        }

        //public static implicit operator global::Global.AuthoritySignedTrans(AuthoritySignedTrans authoSgndTrans)
        //{
        //    return new global::Global.AuthoritySignedTrans(authoSgndTrans.userSgndTrans, authoSgndTrans.PublicKey, authoSgndTrans.SignedData);
        //}
    }
}
