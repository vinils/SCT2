namespace User
{
    using Global;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class Transfer : ITransfer
    {
        private IOrigin[] origins;
        private ITransferId[] transIds;

        public IOrigin[] Origins
        {
            get { return origins; }
        }

        public ITransferId[] TransferIds
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

        public Transfer(IOrigin[] origins, ITransferId[] destinies)
        {
            this.origins = origins;
            this.transIds = destinies;
        }

        public Transfer(IOrigin[] origins, TransferInfo[] destinies)
            : this(origins, destinies.ToTransIds().ToArray())
        {
        }

        public virtual bool IsCreateCoin()
        {
            return false;
        }
    }
}
