// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlDataResponse.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The SQL Data Response.
    /// </summary>
    public sealed class SqlDataResponse
    {
        /// <summary>Gets or sets the last suffix.</summary>
        public int LastSuffix { get; set; }

        /// <summary>Gets or sets the parameters.</summary>
        public List<SqlWhereParam> Params { get; set; }

        /// <summary>Gets or sets the SQL.</summary>
        public string Sql { get; set; }
    }
}