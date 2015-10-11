namespace ScroogeCoin
{
    public interface ITransferHash
    {
        IAuthoritySignedTrans AuthoritySignedTrans { get; }
        byte[] HashCode { get; }
    }
}