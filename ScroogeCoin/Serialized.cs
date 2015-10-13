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
    public abstract class Serialized
    {
        [NonSerialized]
        private object obj;
        [NonSerialized]
        private Bytes srlzdObj;

        protected object Obj
        {
            get { return obj; }
        }

        public Bytes SerializedObj
        {
            get { return srlzdObj; }
        }

        public Serialized(object obj)
        {
            this.obj = obj;
            this.srlzdObj = SerializeObject(this.obj);
        }

        protected Object DeserializeObject()
        {
            return DeserializeObject(srlzdObj);
        }

        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <param name="obj">Object instance</param>
        /// <returns>Serialized object</returns>
        protected static Bytes SerializeObject(object trans)
        {
            Bytes ret;
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
        /// <param name="bytes">Serialized object</param>
        /// <returns>deserialized object</returns>
        protected static object DeserializeObject(Bytes bytes)
        {
            object ret;

            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                ret = bf.Deserialize(ms);
            }

            return ret;
        }
    }
}
