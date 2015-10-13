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
        private Transfer trans;

        public Transfer Transfer
        {
            get { return trans; }
        }

        ITransfer IUserSignedTrans.Transfer
        {
            get { return Transfer; }
        }

        public UserSignedTrans(SerializedTransfer srlzdTrans, Bytes userPk, Bytes userSgndTrans)
            :base(userPk, userSgndTrans)
        {
            this.trans = srlzdTrans.DeserializeTransfer();
        }

        public bool isValidUserSignature(Bytes ownerPk)
        {
            var srlzTrans = new SerializedTransfer(this.trans);
            return this.IsValidSignedMsg(srlzTrans.SerializedObj, ownerPk);
        }

        private bool IsValidSignedMsg(SerializedTransfer srlzdTrans, Bytes publicKey)
        {
            return this.IsValidSignedMsg(srlzdTrans.SerializedObj, publicKey);
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

        public AuthoritySignedTrans(UserSignedTrans userSgndTrans, Bytes authorityPk, Bytes authoSgndTrans)
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

        public bool isValidAuthoritySignature(Bytes ownerPk)
        {
            return this.IsValidSignedMsg(this.userSgndTrans, ownerPk);
        }

        private bool IsValidSignedMsg(UserSignedTrans userSgndTrans, Bytes publicKey)
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

        public TransferHash(Bytes hashCode)
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
    public class TransferInfo 
    {
        private double? value;
        private Bytes destinyPk;

        public double? Value
        {
            get { return value; }
        }

        public Bytes DestinyPk
        {
            get { return destinyPk; }
        }

        public TransferInfo(double? value, Bytes destinyPk)
        {
            this.value = value;
            this.destinyPk = destinyPk;
        }
    }

    [Serializable]
    public class TransferId : TransferInfo, IOriginId
    {
        private int id;

        public int Id
        {
            get { return this.id; }
        }

        public TransferId(int id)
            :this(null, null, id)
        {
        }

        public TransferId(double? value, Bytes destinyPk, int id)
            :base(value, destinyPk)
        {
            this.id = id;
        }

        public static implicit operator TransferId (IOriginId originId)
        {
            return new TransferId(originId.Id);
        }
    }

    public class DestinyIdManage
    {
        private int transCount = 0;
        private TransferId[] destinyIds;

        public TransferId[] DestinyIds
        {
            get { return destinyIds; }
        }

        public DestinyIdManage(TransferInfo[] destinyInfo)
        {
            destinyIds = new TransferId[destinyInfo.Length];
            foreach (var data in destinyInfo)
            {
                var id = ++transCount;
                destinyIds[id] = new TransferId(data.Value, data.DestinyPk, id);
            }
        }

        public static implicit operator TransferId[](DestinyIdManage destinies)
        {
            return destinies.destinyIds;
        }
    }

    [Serializable]
    public class Origin: IOrigin
    {
        private TransferHash hash;
        private TransferId[] origins;

        public TransferHash Hash
        {
            get { return this.hash; }
        }

        ITransferHash IOrigin.Hash
        {
            get { return this.hash; }
        }

        IOriginId[] IOrigin.OriginId
        {
            get { return this.origins; }
        }

        protected Origin (TransferHash hash, TransferId[] originIds)
        {
            this.hash = hash;
            this.origins = originIds;

        }

        public Origin(ITransferHash hash, IOriginId[] originIds)
            :this(
                 new TransferHash(hash.HashCode), 
                 originIds)
        {
            this.hash = new TransferHash(hash.HashCode);

            this.origins = new TransferId[originIds.Length];
            for(int x = 0; x <= originIds.Length; x++)
            {
                this.origins[x] = new TransferId(originIds[x].Id);
            }
            /////// criar um cast (TransferId) IOriginId para fazer automaticamente e para lembrar que isso eh uma conversao e que todos os dados do 
            /////// objeto a deve ser passado para o objeto b


            this.hash 
            this.origins = new TransferId[destinyIds.Length];
            //for(int x = 0; x <= originIds.Length; x++)
            //    this.origins[x] = new TransferId(originIds[x].Id);

        }

    }

    [Serializable]
    public class Transfer : ITransfer
    {
        private Origin[] origins;

        TransferId[] destiniesIds;

        Coin coin;

        public Origin[] Origins
        {
            get { return origins; }
        }

        IOrigin[] ITransfer.Origins
        {
            get { return origins; }
        }

        public TransferId[] Destinies
        {
            get { return destiniesIds; }
        }

        //public Destiny this[int idx]
        //{
        //    get
        //    {
        //        return destinies[idx];
        //    }
        //}

        public Transfer(Coin coin, DestinyIdManage destinies)
        {
            this.coin = coin;
            this.destiniesIds = destinies;
            this.origins = null;
        }

        protected Transfer(IOrigin[] origins, TransferId[] destinies)
        {
            this.coin = null;
            this.origins = new Origin[origins.Length];
            for (int x = 0; x <= origins.Length; x++)
                this.origins[x] = new Origin(origins[x].Hash, origins[x].OriginId);
            this.destiniesIds = destinies;
        }


        public Transfer(IOrigin[] origins, DestinyIdManage destinies)
            :this(origins, (TransferId[])destinies)
        {
        }
    }

    public class Person
    {
        private Signature mySig;
        private const int sizeKey = 256;

        public Bytes PublicKey
        {
            get { return mySig.PublicKey; }
        }

        public Person()
        {
            this.mySig = new Signature(sizeKey);
        }

        //public Transfer PayTo(Transfer trans, Bytes destinyPk)
        //{
        //    var prevHash = new TransferUserSignature(trans, mySig);
        //    return new Transfer(prevHash, destinyPk);
        //}
    }
    public class Authority : Person
    {
        public Transfer CreateCoin(Bytes destinyPk)
        {
            return new Transfer(new Coin(), destinyPk);
        }
    }
}
