namespace Global
{
    using System;

    [Serializable]
    public abstract class NodeId<T>
    {
        private T last;
        private object id;

        public T Last
        {
            get { return this.last; }
            set { this.last = value; }
        }

        public object Id
        {
            get { return this.id; }
        }

        public NodeId(T last, object id)
        {
            this.last = last;
            this.id = id;
        }
    }
}