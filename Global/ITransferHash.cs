namespace Global
{
    public interface ITransferHash
    {
        IAuthoritySignedTrans AuthoritySignedTrans { get; }
        Bytes HashCode { get; }
    }
}