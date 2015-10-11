using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    [Serializable]
    public class HashData:Hash
    {
        [NonSerialized]
        private byte[] data;

        public byte[] Data
        {
            get { return data; }
        }
        public HashData(byte[] data)
            :base(Hash(data))
        {
            this.data = data;
        }

        private static byte[] Hash(byte[] data)
        {
            return new SHA256Managed().ComputeHash(data);
        }
    }

    [Serializable]
    public class Hash
    {
        private byte[] hashCode;

        public byte[] HashCode
        {
            get { return hashCode; }
        }

        //public Hash(byte[] hash)
        //    :base(null)
        //{
        //    this.hashCode = hash;
        //}

        public Hash(byte[] hashCode)
        {
            this.hashCode = hashCode;
        }

        public bool comperHash(byte[] hashComp)
        {
            for (int x = 0; x < this.hashCode.Length; x++)
            {
                if (this.hashCode[x] != hashComp[x])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool comperHash(byte[] hashComp1, byte[] hashComp2)
        {
            for (int x = 0; x < hashComp1.Length; x++)
            {
                if (hashComp1[x] != hashComp2[x])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
