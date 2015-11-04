//-----------------------------------------------------------------------
// <copyright file="Signature.cs" company="VLS">
//     Copyright (c) VLS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Global
{
    using System.Security.Cryptography;

    /// <summary>
    /// ECDSA Signature class
    /// </summary>
    public abstract class Signature
    {
        public enum KeySize { s256 = 256, s384 = 384, s521 = 521 }

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
        public Signature(KeySize keySize)
        {
            this.dsa = new ECDsaCng((int)keySize);
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
        protected SignedMessage SignHash(Bytes hash)
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
        protected Bytes SignMsg(Bytes msg)
        {
            //// signing hash data
            ////var msgHashed = new SHA1Managed().ComputeHash(message);
            ////var sgndData = dsa.SignHash(msgHashed); 

            ////var sgndData = this.dsa.SignData(msg);
            return this.dsa.SignData(msg);
        }
    }
}
