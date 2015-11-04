//-----------------------------------------------------------------------
// <copyright file="Global.cs" company="VLS">
//     Copyright (c) VLS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Global
{
    using System.Security.Cryptography;

    /// <summary>
    /// Global properties and functions
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Hash Algorithm
        /// </summary>
        public static readonly CngAlgorithm HashAlgorithm = CngAlgorithm.Sha256;

        /// <summary>
        /// Goofy public key
        /// </summary>
        private static Bytes goofyPk;

        /// <summary>
        /// Gets or sets goofy public key
        /// </summary>
        public static Bytes ScroogePk
        {
            get { return goofyPk; }
            set { goofyPk = value; }
        }
    }
}
