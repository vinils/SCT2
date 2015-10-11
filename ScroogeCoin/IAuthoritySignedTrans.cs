namespace ScroogeCoin
{
    public interface IAuthoritySignedTrans
    {
        IUserSignedTrans UserSignedTransfer { get; }

        bool isValidAuthoritySignature(byte[] ownerPk);
    }
}