//-----------------------------------------------------------------------
// <copyright file="Signature.cs" company="VLS">
//     Copyright (c) VLS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ScroogeCoin
{
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
        private byte[] publicKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="sizeKey">Size of the ECDSA key</param>
        public Signature(int sizeKey)
        {
            this.dsa = new ECDsaCng(sizeKey);
            this.dsa.HashAlgorithm = Global.HashAlgorithm;
            this.publicKey = this.dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob);
        }

        /// <summary>
        /// Gets or sets Public key
        /// </summary>
        public byte[] PublicKey
        {
            get { return this.publicKey; }
            protected set { this.publicKey = value; }
        }

        /// <summary>
        /// Sign a hash
        /// </summary>
        /// <param name="hash">hash information</param>
        /// <returns>return signed hash</returns>
        public SignedTransfer SignHash(byte[] hash)
        {
            //// signing hash data
            ////var msgHashed = new SHA1Managed().ComputeHash(message);
            ////var sgndData = dsa.SignHash(msgHashed); 

            ////var sgndData = this.dsa.SignData(msg);
            var sgndData = this.dsa.SignHash(hash);
            return new SignedTransfer(this.publicKey, sgndData);
        }

        ///// <summary>
        ///// Sign a message
        ///// </summary>
        ///// <param name="msg">Message instance</param>
        ///// <returns>Signed message</returns>
        //protected SignedMessage SignMsg(byte[] msg)
        //{
        //    //// signing hash data
        //    ////var msgHashed = new SHA1Managed().ComputeHash(message);
        //    ////var sgndData = dsa.SignHash(msgHashed); 

        //    ////var sgndData = this.dsa.SignData(msg);
        //    var sgndData = this.dsa.SignData(msg);
        //    return new SignedMessage(this.publicKey, sgndData);
        //}
    }
}
