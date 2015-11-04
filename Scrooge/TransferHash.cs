using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrooge
{
    using Global;

    [Serializable]
    public class TransferHash: Hash, ITransferHash
    {
        private AuthoritySignedTrans authoSgndTrans;

        public AuthoritySignedTrans AuthoritySignedTrans
        {
            get { return authoSgndTrans; }
        }

        IAuthoritySignedTrans ITransferHash.AuthoritySignedTrans
        {
            get { return authoSgndTrans; }
        }

        protected TransferHash(Bytes hashCode)
            : base(hashCode)
        {
        }

        public TransferHash(AuthoritySignedTrans authoSgndTrans)
            : this(new HashData(authoSgndTrans.SignedData).HashCode)
        {
            this.authoSgndTrans = authoSgndTrans;
        }

        //public static implicit operator global::Global.TransferHash(TransferHash transHash)
        //{
        //    return new global::Global.TransferHash(transHash.authoSgndTrans);
        //}
    }
}
