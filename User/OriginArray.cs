namespace User
{
    using Global;
    using System;

    [Serializable]
    public class OriginArray : IOrigin
    {
        private ITransferHash hash;
        private IOriginId[] transIds;

        public ITransferHash Hash
        {
            get { return this.hash; }
        }

        ITransferHash IOrigin.Hash
        {
            get { return this.hash; }
        }

        public IOriginId[] TransfesIds
        {
            get { return this.transIds; }
        }

        IOriginId[] IOrigin.OriginIds
        {
            get { return this.transIds; }
        }

        public OriginArray(ITransferHash hash, IOriginId[] transIds)
        {
            this.hash = hash;
            this.transIds = transIds;
        }

        public OriginArray(OriginNode node)
            :this(node.Hash, node.TransferIds.ToArray())
        {
        }
    }
}
