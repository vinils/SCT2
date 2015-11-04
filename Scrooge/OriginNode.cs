namespace Scrooge
{
    using Global;

    public class OriginNode: NodeId<OriginNode>
    {
        private TransferHash hash;
        private TransferIdLinkedList transId;

        public OriginNode(OriginNode last, TransferHash hash, TransferIdLinkedList transId)
            : base(last, hash.HashCode)
        {
            this.hash = hash;
            this.transId = transId;
        }

        public TransferHash Hash
        {
            get { return this.hash; }
        }

        public new Bytes Id
        {
            get { return (Bytes)base.Id; }
        }

        //ITransferHash IOrigin.Hash
        //{
        //    get { return this.hash; }
        //}

        public TransferIdLinkedList TransferIds
        {
            get { return this.transId; }
        }

        //IOriginId[] IOrigin.OriginIds
        //{
        //    get { return this.transId; }
        //}

        public TransferIdNode FindTransferId(int id)
        {
            return this.transId.Find(id);
        }

        public void Remove(int id)
        {
            this.transId.Remove(id);
        }

        public bool IsSameDestiny()
        {
            if (!TransferIds.IsSameDestiny())
                return false;

            return true;
        }
    }
}
