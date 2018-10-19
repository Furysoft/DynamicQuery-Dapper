// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWhereFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces.Formatters
{
    using System.Collections.Generic;
    using DynamicQuery.Entities.Nodes;
    using Entities;

    /// <summary>
    /// The Where Formatter Interface
    /// </summary>
    public interface IWhereFormatter
    {
        /// <summary>
        /// Formats the specified where node.
        /// </summary>
        /// <param name="whereNode">The where node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>The <see cref="SqlDataResponse"/></returns>
        SqlDataResponse Format(Node whereNode, IDictionary<string, object> dataDictionary);
    }
}