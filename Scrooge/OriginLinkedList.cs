namespace Scrooge
{
    using System.Collections.Generic;
    using Global;

    public class OriginLinkedList: global::Global.LinkedList<OriginNode>, IEnumerable<OriginNode>
    {
        public OriginLinkedList()
            :base()
        {
        }

        public OriginLinkedList(OriginNode head)
            :base(head)
        {
        }

        public void Add(TransferHash hash, TransferIdLinkedList transId)
        {
            Head = new OriginNode(Head, hash, transId);
        }

        public OriginNode Find(Bytes hashCode)
        {
            return base.Find(hashCode);
        }

        public void Remove(TransferHash hash)
        {
            base.Remove(hash.HashCode);
        }

        public bool IsSameDestiny()
        {
            if (!Head.IsSameDestiny())
                return false;

            return true;
        }

        public OriginArray[] ToOriginArray()
        {
            var count = this.Count;
            var arrIds = new OriginArray[count];

            foreach (var node in this)
            {
                arrIds[--count] = new OriginArray(node);
            }

            return arrIds;
        }
    }
}
