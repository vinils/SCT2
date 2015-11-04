using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class UserRequest
    {
        private SerializedUserSignedTrans srlzdUsrSgndTrans;
        private SerializedTransfer srlzdTrans;

        public SerializedUserSignedTrans SrlzdUsrSgndTrans
        {
            get { return srlzdUsrSgndTrans; }
        }

        public SerializedTransfer SrlzdTrans
        {
            get { return srlzdTrans; }
        }

        public UserRequest(SerializedUserSignedTrans srlzdUsrSgndTrans, SerializedTransfer srlzdTrans)
        {
            this.srlzdTrans = srlzdTrans;
            this.srlzdUsrSgndTrans = srlzdUsrSgndTrans;
        }
    }
}
