namespace Scrooge
{
    using Global;
    using System;

    [Serializable]
    public class Transfer: ITransfer
    {
        private OriginArray[] origins;
        private ITransferId[] transIds;

        [NonSerialized]
        Coin coin;

        public OriginArray[] Origins
        {
            get { return origins; }
        }

        IOrigin[] ITransfer.Origins
        {
            get { return origins; }
        }

        public ITransferId[] TransferIds
        {
            get { return transIds; }
        }

        ITransferId[] ITransfer.TransferIds
        {
            get { return transIds; }
        }

        //public Destiny this[int idx]
        //{
        //    get
        //    {
        //        return destinies[idx];
        //    }
        //}

        public Transfer(Coin coin, ITransferId[] transId)
        {
            this.origins = null;
            this.transIds = transId;
            this.coin = coin;
        }

        public Transfer(OriginArray[] origins, ITransferId[] destinies)
        {
            this.origins = origins;
            this.transIds = destinies;
            this.coin = null;
        }

        public bool IsCreateCoin()
        {
            return coin != null;
        }

        //public static Origin[] FindTransfer(global::Global.TransferHash[] lastHash, global::Global.Origin[] origin)
        //{
        //    /// basear-se em origin findTransferIds


        //    //var transIds = new Origin[origin.Length];
        //    //for (int x = 0; x <= transIds.Length; x++)
        //    //{
        //    //    transIds[x] = lastHash[x].AuthoritySignedTransA.UserSignedTransfer.Transfer.Destinies[ids[x].Id];
        //    //}

        //    //return transIds;

        //    //lastHash.AuthoritySignedTrans.UserSignedTransfer.Transfer.Origins

        //    throw new Exception("Not implemented!");
        //}

        //public static implicit operator global::Global.Transfer(Transfer trans)
        //{
        //    return new global::Global.Transfer(trans.origins.ToGlobal(), trans.destiniesIds);
        //}
    }
}
