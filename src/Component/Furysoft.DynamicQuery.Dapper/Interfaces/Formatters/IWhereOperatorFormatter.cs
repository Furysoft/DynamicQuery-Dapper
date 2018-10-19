// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWhereOperatorFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces.Formatters
{
    using System.Collections.Generic;
    using DynamicQuery.Entities.Nodes;
    using Entities;

    /// <summary>
    /// The Where Operator Formatter
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    public interface IWhereOperatorFormatter<in TType>
        where TType : UnaryNode
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>The <see cref="SqlDataResponse"/></returns>
        SqlDataResponse Format(TType node, IDictionary<string, object> dataDictionary);
    }
}