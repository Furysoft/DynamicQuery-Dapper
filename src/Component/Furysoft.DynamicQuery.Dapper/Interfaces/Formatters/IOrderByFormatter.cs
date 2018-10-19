// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrderByFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces.Formatters
{
    using System.Collections.Generic;
    using DynamicQuery.Entities.QueryComponents;

    /// <summary>
    /// The Order By Formatter Interface
    /// </summary>
    public interface IOrderByFormatter
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The order by string</returns>
        string Format(List<OrderByNode> node);
    }
}