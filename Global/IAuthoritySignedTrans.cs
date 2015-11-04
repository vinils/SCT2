namespace Global
{
    public interface IAuthoritySignedTrans
    {
        ITransferHash Hash { get; }

        bool isValidAuthoritySignature(Bytes ownerPk);
    }
}