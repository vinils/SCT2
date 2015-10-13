namespace ScroogeCoin
{
    public interface ITransferHash
    {
        IAuthoritySignedTrans AuthoritySignedTrans { get; }
        Bytes HashCode { get; }
    }
}