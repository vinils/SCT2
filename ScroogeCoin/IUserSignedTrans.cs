namespace ScroogeCoin
{
    public interface IUserSignedTrans
    {
        ISerializedTransfer SerializedTransfer { get; }

        bool isValidUserSignature(byte[] ownerPk);
    }
}