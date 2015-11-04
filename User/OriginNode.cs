namespace User
{
    using System;
    using Global;

    [Serializable]
    public class OriginNode: NodeId<OriginNode>
    {
        private ITransferHash hash;
        private TransferIdLinkedList transId;

        public OriginNode(OriginNode last, ITransferHash hash, TransferIdLinkedList transId)
            : base(last, hash.HashCode)
        {
            this.hash = hash;
            this.transId = transId;
        }

        public ITransferHash Hash
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
        //    get { return this.transId.Array; }
        //}

        public TransferIdNode FindTransferId(int id)
        {
            return this.transId.Find(id);
        }

        public void Remove(int id)
        {
            this.transId.Remove(id);
        }
    }
}
