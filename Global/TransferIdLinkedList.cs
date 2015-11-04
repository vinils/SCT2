namespace Global
{
    using System.Collections.Generic;
    using System;

    [Serializable]
    public class TransferIdLinkedList: LinkedList<TransferIdNode>, IEnumerable<TransferIdNode>
    {
        public TransferIdLinkedList()
            :base()
        {
        }

        public TransferIdLinkedList(TransferIdNode head)
            :base(head)
        {
        }

        public void Add(double value, Bytes destinyPk, int id)
        {
            Head = new TransferIdNode(Head, value, destinyPk, id);
        }

        public TransferIdNode Find(int id)
        {
            return base.Find(id);
        }

        public void Remove(int id)
        {
            base.Remove(id);
        }

        //public TransferIdNode[] ToArray()
        //{
        //    return base.ToArray();
        //}

        public bool IsSameDestiny()
        {
            var destinyPk = this.Head.DestinyPk;

            foreach (var node in this)
            {
                if (node.DestinyPk != destinyPk)
                    return false;
            }

            return true;
        }

        public static implicit operator TransferIdLinkedList(TransferIdNode transIdNode)
        {
            return transIdNode.ToLinkedList();
        }
    }
}
