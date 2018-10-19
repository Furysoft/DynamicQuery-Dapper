// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPageFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces.Formatters
{
    using System.Collections.Generic;
    using DynamicQuery.Entities.QueryComponents;
    using Entities;

    /// <summary>
    /// The Page Formatter Interface
    /// </summary>
    public interface IPageFormatter
    {
        /// <summary>
        /// Formats the specified page node.
        /// </summary>
        /// <param name="pageNode">The page node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>The <see cref="SqlDataResponse" /></returns>
        SqlDataResponse Format(PageNode pageNode, IDictionary<string, object> dataDictionary);
    }
}