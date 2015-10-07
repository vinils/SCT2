using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    [Serializable]
    public class TransferHash
    {
        [NonSerialized]
        private Transfer transfer;

        [NonSerialized]
        private byte[] transHash;

        [NonSerialized]
        private SignedTransfer sgndTrans;

        private byte[] hashSgndTrans;

        public byte[] HashSgndTrans
        {
            get { return hashSgndTrans; }
        }

        public TransferHash(Transfer trans, Signature mySignature)
        {
            this.transfer = trans;
            var serializedTrans = SerializeObject(trans);
            this.transHash = Hash(serializedTrans);
            this.sgndTrans = mySignature.SignHash(transHash);
            this.hashSgndTrans = Hash(sgndTrans.SignedData);
        }

        public bool isValidHash()
        {
            var serializedObj = SerializeObject(transfer);
            var hash = Hash(serializedObj);
            var bo = sgndTrans.IsValidSignedHash(hash, transfer.DestinyPk);
            return true;
        }

        private byte[] Hash(byte[] info)
        {
            return new SHA256Managed().ComputeHash(info, 0, info.Length);
        }

        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <param name="obj">Object instance</param>
        /// <returns>Serialized object</returns>
        private static byte[] SerializeObject(object obj)
        {
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ret = ms.ToArray();
            }

            return ret;
        }
    }

    [Serializable]
    public class Transfer
    {
        private TransferHash previous;

        byte[] destinyPk;

        Coin coin;

        public TransferHash Previous
        {
            get { return previous; }
        }

        public byte[] DestinyPk
        {
            get { return destinyPk; }
        }

        public Transfer(Coin coin, byte[] destinyPk)
        {
            this.coin = coin;
            this.destinyPk = destinyPk;
            this.previous = null;
        }

        public Transfer(TransferHash previous, byte[] destinyPk)
        {
            this.coin = null;
            this.destinyPk = destinyPk;
            this.previous = previous;
        }
    }

    public class Person
    {
        private Signature mySig;
        private const int sizeKey = 256;

        public byte[] PublicKey
        {
            get { return mySig.PublicKey; }
        }

        public Person()
        {
            this.mySig = new Signature(sizeKey);
        }

        public Transfer PayTo(Transfer trans, byte[] destinyPk)
        {
            var prevHash = new TransferHash(trans, mySig);
            return new Transfer(prevHash, destinyPk);
        }
    }
    public class Authority : Person
    {
        public Transfer CreateCoin(byte[] destinyPk)
        {
            return new Transfer(new Coin(), destinyPk); 
        }
    }
}
