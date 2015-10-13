namespace ScroogeCoin
{
    public interface IUserSignedTrans
    {
        ITransfer Transfer { get; }

        bool isValidUserSignature(Bytes ownerPk);
    }
}