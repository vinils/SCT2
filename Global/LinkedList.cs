using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    [Serializable]
    public abstract class LinkedList<T> : IEnumerable<T> where T : NodeId<T>
    {
        [NonSerialized]
        private T head;

        protected virtual T Head
        {
            get { return this.head; }
            set { this.head = value; }
        }

        public int Count
        {
            get
            {
                var count = 0;

                foreach (var node in this)
                    count++;

                return count;
            }
        }

        public LinkedList()
        {
            this.head = null;
        }

        public LinkedList(T head)
        {
            this.head = head;
        }

        protected virtual T Find(object id)
        {
            foreach (var headNode in this)
            {
                if (headNode.Id.Equals(id))
                    return headNode;
            }

            throw new Exception("Node Id not founded");
        }

        private T FindPrevious(object id)
        {
            T previous = null;

            foreach (var headNode in this)
            {
                if (headNode.Id.Equals(id))
                    return previous;

                previous = headNode;
            }

            throw new Exception("Node Id not founded");
        }

        protected void Remove(object id)
        {
            var prev = this.FindPrevious(id);

            if(prev == null)
            {
                this.head = this.head.Last;
            }
            else
            {
                prev.Last = prev.Last.Last;
            }
        }

        /// <summary>
        /// append this linked list to someone and return the head node
        /// </summary>
        /// <param name="linkedList">linked list where the begginning of this list will be attached</param>
        /// <returns>head node</returns>
        protected T AppendTo(LinkedList<T> linkedList)
        {
#warning must have a better way other than loop to find the first node this is a logic problem in concern with the logic code structure maybe will be automatic fixed with a tree instead of linked list
            foreach (var node in this)
            {
                if (node.Last == null)
                {
                    node.Last = linkedList.head;
                    break;
                }
            }

            return this.head;
        }

        /// <summary>
        /// append some linked list to this one
        /// </summary>
        /// <param name="linkedList">linked list to be appended</param>
        public void Append(LinkedList<T> linkedList)
        {
            head = linkedList.AppendTo(this);
        }

        public void MoveTo(LinkedList<T> linkedList)
        {
            linkedList.Append(this);
            this.head = null;
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            var current = this.Head;

            while (current != null)
            {
                yield return current;
                current = current.Last;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public T[] ToArray()
        {
            var count = this.Count;
            var arrIds = new T[count];

            foreach (var node in this)
            {
                arrIds[--count] = node;
            }

            return arrIds;
        }
    }
}
