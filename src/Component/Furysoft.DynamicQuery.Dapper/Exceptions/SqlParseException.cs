// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlParseException.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Exceptions
{
    using System;

    /// <summary>
    /// The SQL Parse Exception.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public sealed class SqlParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParseException"/> class.
        /// </summary>
        public SqlParseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParseException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SqlParseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public SqlParseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}