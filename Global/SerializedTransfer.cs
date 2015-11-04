namespace Global
{
    using System;

    [Serializable]
    public class SerializedTransfer : Serialized
    {
        public SerializedTransfer(ITransfer trans)
            : base(trans)
        {
        }

        public ITransfer DeserializeTransfer()
        {
            return (ITransfer)DeserializeObject(this.SerializedObj);
        }
    }
}
