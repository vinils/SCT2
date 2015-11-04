namespace Global
{
    using System;
    using System.Security.Cryptography;

    [Serializable]
    public class HashData:Hash
    {
        [NonSerialized]
        private Bytes data;

        public Bytes Data
        {
            get { return data; }
        }

        public HashData(Bytes data)
            :base(Hash(data))
        {
            this.data = data;
        }

        private static Bytes Hash(Bytes data)
        {
            return new SHA256Managed().ComputeHash(data);
        }
    }

    [Serializable]
    public class Hash
    {
        private Bytes hashCode;

        public Bytes HashCode
        {
            get { return hashCode; }
        }

        public Hash(Bytes hashCode)
        {
            this.hashCode = hashCode;
        }
    }
}
