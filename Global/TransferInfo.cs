namespace Global
{
    using System;

    [Serializable]
    public class TransferInfo
    {
        private double value;
        private Bytes destinyPk;

        public double Value
        {
            get { return value; }
        }

        public Bytes DestinyPk
        {
            get { return destinyPk; }
        }

        public TransferInfo(double value, Bytes destinyPk)
        {
            this.value = value;
            this.destinyPk = destinyPk;
        }
    }
}
