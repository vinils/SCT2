using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class Signature : Global.Signature
    {
        public Signature(KeySize keySize)
            :base(keySize)
        {
        }

        public UserSignedTrans SignMsg(Transfer trans)
        {
            var srlzdTrans = new SerializedTransfer(trans);
            return new UserSignedTrans(PublicKey, this.SignMsg(srlzdTrans.SerializedObj));
        }
    }
}
