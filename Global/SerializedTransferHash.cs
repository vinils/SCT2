using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class SerializedTransferHash : Serialized
    {
        public SerializedTransferHash(ITransferHash transHash)
            : base(transHash)
        {
        }

        public ITransferHash DeserializeTransfer()
        {
            return (ITransferHash)DeserializeObject(this.SerializedObj);
        }
    }
}
