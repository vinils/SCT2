namespace Global
{
    public interface ITransferId
    {
        double Value { get; }
        Bytes DestinyPk { get; }
        int Id { get; }
    }
}