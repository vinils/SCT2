namespace ScroogeCoin
{
    public interface ITransfer
    {
        IOrigin[] Origins { get; }
        //Destiny this[int idx] { get; }
        TransferId[] Destinies { get; }
    }
}