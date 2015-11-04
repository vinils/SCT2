namespace Global
{
    using System;
    
    [Serializable]
    public class Bytes
    {
        private byte[] bytes;

        public int Length
        {
            get { return this.bytes.Length; }
        }

        public Bytes(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public static bool Compare(byte[] bytes1, byte[] bytes2)
        {
            for (int x = 0; x < bytes1.Length; x++)
            {
                if (bytes1[x] != bytes2[x])
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Bytes;
            if (item == null)
                return false;
            return Compare(bytes, item.bytes);
        }

        public override int GetHashCode()
        {
            throw new Exception("Not implemented");
            //int hash = 17;
            //unchecked
            //{
            //    foreach (var element in bytes)
            //    {
            //        hash = hash * 31 + element.GetHashCode();
            //    }
            //}
            //return hash;
        }

        public static bool operator ==(Bytes a, Bytes b)
        {
            //if (System.Object.ReferenceEquals(a, b))
            //    return true;
            if (((object)a == null) || ((object)b == null))
                return false;
            return Compare(a.bytes, b.bytes);
        }

        public static bool operator !=(Bytes a, Bytes b)
        {
            return !(a == b);
        }

        public static implicit operator byte[] (Bytes bytes)
        {
            return bytes.bytes;
        }

        public static implicit operator Bytes(byte[] bytes)
        {
            return new Bytes(bytes);
        }
    }
}
