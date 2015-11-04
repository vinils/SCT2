namespace Scrooge
{
    using Global;
    using System;

    [Serializable]
    public class OriginArray : IOrigin
    {
        private TransferHash hash;
        private IOriginId[] transIds;

        public TransferHash Hash
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

        public OriginArray(TransferHash hash, IOriginId[] transIds)
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
