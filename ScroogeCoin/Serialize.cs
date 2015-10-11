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
    public class SerializedTransfer : ISerializedTransfer
    {
        [NonSerialized]
        private Transfer trans;
        [NonSerialized]
        private byte[] srlzdTrans;

        public Transfer Transfer
        {
            get { return trans; }
        }

        public byte[] SerializedTransBytes
        {
            get { return srlzdTrans; }
        }

        ITransfer ISerializedTransfer.Transfer
        {
            get { return Transfer; }
        }

        public SerializedTransfer(Transfer trans)
        {
            this.trans = trans;
            this.srlzdTrans = SerializeObject(this.trans);
        }

        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <param name="obj">Object instance</param>
        /// <returns>Serialized object</returns>
        private static byte[] SerializeObject(Transfer trans)
        {
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, trans);
                ret = ms.ToArray();
            }

            return ret;
        }

        /// <summary>
        /// Deserialize an object
        /// </summary>
        /// <param name="byts">Serialized object</param>
        /// <returns>deserialized object</returns>
        private static object DeserializeObject(byte[] byts)
        {
            object ret;

            using (var ms = new System.IO.MemoryStream())
            {
                var bf = new BinaryFormatter();
                ms.Write(byts, 0, byts.Length);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                ret = bf.Deserialize(ms);
            }

            return ret;
        }
    }
}
