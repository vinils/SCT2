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
    public class UserSignedTrans : SignedMessage, IUserSignedTrans
    {
        [NonSerialized]
        private SerializedTransfer srlzdTrans;

        public SerializedTransfer SerializedTransfer
        {
            get { return srlzdTrans; }
        }

        ISerializedTransfer IUserSignedTrans.SerializedTransfer
        {
            get { return SerializedTransfer; }
        }

        public UserSignedTrans(SerializedTransfer trans, byte[] userPk, byte[] userSgndTrans)
            :base(userPk, userSgndTrans)
        {
            this.srlzdTrans = trans;
        }

        public bool isValidUserSignature(byte[] ownerPk)
        {
            return this.IsValidSignedMsg(this.srlzdTrans, ownerPk);
        }

        private bool IsValidSignedMsg(SerializedTransfer srlzdTrans, byte[] publicKey)
        {
            return this.IsValidSignedMsg(srlzdTrans.SerializedTransBytes, publicKey);
        }
    }

    [Serializable]
    public class AuthoritySignedTrans : SignedMessage, IAuthoritySignedTrans
    {
        [NonSerialized]
        private UserSignedTrans userSgndTrans;

        public UserSignedTrans UserSignedTransfer
        {
            get { return userSgndTrans; }
        }

        IUserSignedTrans IAuthoritySignedTrans.UserSignedTransfer
        {
            get { return this.UserSignedTransfer; }
        }

        public TransferHash Hash
        {
            get { return new TransferHash(this); }
        }

        public AuthoritySignedTrans(UserSignedTrans userSgndTrans, byte[] authorityPk, byte[] authoSgndTrans)
            : base(authorityPk, authoSgndTrans)
        {
            this.userSgndTrans = userSgndTrans;
        }

        //public bool isValidHash()
        //{
        //    var serializedObj = SerializeObject(transfer);
        //    var hash = Hash(serializedObj);
        //    var bo = sgndTrans.IsValidSignedHash(hash, transfer.DestinyPk);
        //    return true;
        //}

        public bool isValidAuthoritySignature(byte[] ownerPk)
        {
            return this.IsValidSignedMsg(this.userSgndTrans, ownerPk);
        }

        private bool IsValidSignedMsg(UserSignedTrans userSgndTrans, byte[] publicKey)
        {
            return this.IsValidSignedMsg(userSgndTrans.SignedData, publicKey);
        }

    }

    [Serializable]
    public class TransferHash : Hash, ITransferHash
    {
        [NonSerialized]
        private AuthoritySignedTrans authoSgndTrans;

        public AuthoritySignedTrans AuthoritySignedTrans
        {
            get { return authoSgndTrans; }
        }

        IAuthoritySignedTrans ITransferHash.AuthoritySignedTrans
        {
            get { return this.AuthoritySignedTrans; }
        }

        public TransferHash(byte[] hashCode)
            : base(hashCode)
        {
        }

        public TransferHash(AuthoritySignedTrans authoSgndTrans)
            :this(new HashData(authoSgndTrans.SignedData).HashCode)
        {
            this.authoSgndTrans = authoSgndTrans;
        }
    }

    [Serializable]
    public class Transfer : ITransfer
    {
        private TransferHash previousHash;

        byte[] destinyPk;

        Coin coin;

        public TransferHash Previous
        {
            get { return previousHash; }
        }

        byte[] ITransfer.Previous
        {
            get { return Previous.HashCode; }
        }

        public byte[] DestinyPk
        {
            get { return destinyPk; }
        }

        public Transfer(Coin coin, byte[] destinyPk)
        {
            this.coin = coin;
            this.destinyPk = destinyPk;
            this.previousHash = null;
        }

        public Transfer(ITransferHash previous, byte[] destinyPk)
        {
            this.coin = null;
            this.destinyPk = destinyPk;
            this.previousHash = new TransferHash(previous.HashCode);
        }

        public Transfer(TransferHash previous, ITransfer trans)
            :this(previous, trans.DestinyPk)
        {
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

        //public Transfer PayTo(Transfer trans, byte[] destinyPk)
        //{
        //    var prevHash = new TransferUserSignature(trans, mySig);
        //    return new Transfer(prevHash, destinyPk);
        //}
    }
    public class Authority : Person
    {
        public Transfer CreateCoin(byte[] destinyPk)
        {
            return new Transfer(new Coin(), destinyPk);
        }
    }
}
