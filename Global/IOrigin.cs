namespace Global
{
    public interface IOrigin
    {
        ITransferHash Hash { get; }
        IOriginId[] OriginIds { get; }
    }
}