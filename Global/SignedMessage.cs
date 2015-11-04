﻿//-----------------------------------------------------------------------
// <copyright file="SignedTransfer.cs" company="VLS">
//     Copyright (c) VLS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Global
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Signed Transfer with the public key and the signed data
    /// </summary>
    [Serializable]
    public class SignedMessage
    {
        /// <summary>
        /// Signed data
        /// </summary>
        protected Bytes sgndData;

        /// <summary>
        /// Public key
        /// </summary>
        private Bytes publicKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedMessage"/> class.
        /// </summary>
        /// <param name="publicKey">Public key</param>
        /// <param name="sgndData">Signed data</param>
        public SignedMessage(Bytes publicKey, Bytes sgndData)
        {
            this.publicKey = publicKey;
            this.sgndData = sgndData;
        }

        /// <summary>
        /// Gets public key
        /// </summary>
        public Bytes PublicKey
        {
            get { return this.publicKey; }
        }

        /// <summary>
        /// Gets signed data
        /// </summary>
        public Bytes SignedData
        {
            get { return this.sgndData; }
        }

        /// <summary>
        /// Is public key null?
        /// </summary>
        /// <returns>return true if the public key is null</returns>
        public virtual bool IsPublicKeyNull()
        {
            return this.publicKey == null;
        }

        /// <summary>
        /// Is signed data of the Transfer null?
        /// </summary>
        /// <returns>return true if signed data of the Transfer is null</returns>
        public virtual bool IsSignedDataNull()
        {
            return this.sgndData == null;
        }

        /// <summary>
        /// check is the signed Transfer is valid
        /// </summary>
        public virtual void Check()
        {
            if (this.IsPublicKeyNull())
            {
                throw new Exception("Signed Transfer must to have a public key.");
            }

            if (this.IsSignedDataNull())
            {
                throw new Exception("Signed data must be informed.");
            }
        }

        /// <summary>
        /// Is the signed hash belong to the hash and public key
        /// </summary>
        /// <param name="hash">transfer info hash</param>
        /// <param name="publicKey">public key</param>
        /// <returns>true if the signed hash belong to the hash and public key</returns>
        protected bool IsValidSignedHash(Bytes hash, Bytes publicKey)
        {
            bool ret;

            using (var dsa = new ECDsaCng(CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob)))
            {
                dsa.HashAlgorithm = Global.HashAlgorithm;

                //// verifying hashed message
                ////bReturn = dsa.VerifyHash(dataHash, SignedMsg);
                ret = dsa.VerifyHash(hash, this.sgndData);
            }

            return ret;
        }

        protected bool IsValidSignedMsg(Bytes msg, Bytes publicKey)
        {
            bool ret;

            using (var dsa = new ECDsaCng(CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob)))
            {
                dsa.HashAlgorithm = Global.HashAlgorithm;

                //// verifying hashed message
                ////bReturn = dsa.VerifyHash(dataHash, SignedMsg);
                ret = dsa.VerifyData(msg, this.sgndData);
            }

            return ret;
        }
    }
}