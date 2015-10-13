using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    [Serializable]
    public class SerializedTransfer : Serialized
    {
        //ITransfer ISerializedTransfer.Transfer
        //{
        //    get { return Transfer; }
        //}

        public SerializedTransfer(Transfer trans)
            : base(trans)
        {
        }

        public Transfer DeserializeTransfer()
        {
            return (Transfer)DeserializeObject(this.SerializedObj);
        }
    }
}
