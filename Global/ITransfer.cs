namespace Global
{
    public interface ITransfer
    {
        IOrigin[] Origins { get; }
        ITransferId[] TransferIds { get; }

        bool IsCreateCoin();
    }
}