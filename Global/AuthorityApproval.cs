using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class AuthorityApproval
    {
        private SerializedTransferHash srlzdTransferHash;
        private SerializedTransfer srlzdTrans;

        public SerializedTransferHash SrlzdTransferHash
        {
            get { return srlzdTransferHash; }
        }

        public SerializedTransfer SrlzdTrans
        {
            get { return srlzdTrans; }
        }

        public AuthorityApproval(SerializedTransferHash srlzdTransferHash, SerializedTransfer srlzdTrans)
        {
            this.srlzdTrans = srlzdTrans;
            this.srlzdTransferHash = srlzdTransferHash;
        }
    }
}
