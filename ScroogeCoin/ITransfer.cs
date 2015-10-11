namespace ScroogeCoin
{
    public interface ITransfer
    {
        byte[] DestinyPk { get; }
        byte[] Previous { get; }
    }
}