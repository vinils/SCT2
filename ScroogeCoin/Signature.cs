//-----------------------------------------------------------------------
// <copyright file="Signature.cs" company="VLS">
//     Copyright (c) VLS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ScroogeCoin
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Security.Cryptography;

    /// <summary>
    /// ECDSA Signature class
    /// </summary>
    public class Signature
    {
        /// <summary>
        /// ECDSA instance class
        /// </summary>
        private ECDsaCng dsa;

        /// <summary>
        /// Public key
        /// </summary>
        private Bytes publicKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="sizeKey">Size of the ECDSA key</param>
        public Signature(int sizeKey)
        {
            this.dsa = new ECDsaCng(sizeKey);
            this.dsa.HashAlgorithm = Global.HashAlgorithm;
            this.publicKey = new Bytes(this.dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob));
        }

        /// <summary>
        /// Gets or sets Public key
        /// </summary>
        public Bytes PublicKey
        {
            get { return this.publicKey; }
            protected set { this.publicKey = value; }
        }

        /// <summary>
        /// Sign a hash
        /// </summary>
        /// <param name="hash">hash information</param>
        /// <returns>return signed hash</returns>
        public SignedMessage SignHash(Bytes hash)
        {
            //// signing hash data
            ////var msgHashed = new SHA1Managed().ComputeHash(message);
            ////var sgndData = dsa.SignHash(msgHashed); 

            ////var sgndData = this.dsa.SignData(msg);
            var sgndData = this.dsa.SignHash(hash);
            return new SignedMessage(this.publicKey, sgndData);
        }

        /// <summary>
        /// Sign a message
        /// </summary>
        /// <param name="msg">Message instance</param>
        /// <returns>Signed message</returns>
        private Bytes SignMsg(Bytes msg)
        {
            //// signing hash data
            ////var msgHashed = new SHA1Managed().ComputeHash(message);
            ////var sgndData = dsa.SignHash(msgHashed); 

            ////var sgndData = this.dsa.SignData(msg);
            return this.dsa.SignData(msg);
        }

        private Bytes SignMsg(SerializedTransfer srlzdTrans)
        {
            return this.SignMsg(srlzdTrans.SerializedTransBytes);
        }

        private Bytes SignMsg(UserSignedTrans userSgndTrans)
        {
            return this.SignMsg(userSgndTrans.SignedData);
        }

        public UserSignedTrans SignTransfer(SerializedTransfer srlzdTrans)
        {
            return new UserSignedTrans(srlzdTrans, this.publicKey, this.SignMsg(srlzdTrans));
        }

        public AuthoritySignedTrans SignTransfer(UserSignedTrans userSgndTrans)
        {
            return new AuthoritySignedTrans(userSgndTrans, publicKey, this.SignMsg(userSgndTrans));
        }
    }
}
