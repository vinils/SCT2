namespace Global
{
    using System;

    [Serializable]
    public class TransferIdNode: NodeId<TransferIdNode>, ITransferId, IOriginId
    {
        private Bytes destinyPk;
        private double value;

        public TransferIdNode(TransferIdNode last, double value, Bytes destinyPk, int id)
            : base(last, id)
        {
            this.destinyPk = destinyPk;
            this.value = value;
        }

        public Bytes DestinyPk
        {
            get { return this.destinyPk; }
        }

        public double Value
        {
            get { return this.value; }
        }

        public new int Id
        {
            get { return (int)base.Id; }
        }

        public TransferIdLinkedList ToLinkedList()
        {
            return new TransferIdLinkedList(this);
        }
    }
}
