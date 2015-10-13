using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    public class SerializedITransferHash : Serialized
    {
        public SerializedITransferHash(ITransferHash iTransHash)
            : base(iTransHash)
        {
        }

        public ITransferHash DeserializeTransfer()
        {
            return (ITransferHash)DeserializeObject(this.SerializedObj);
        }
    }
}
