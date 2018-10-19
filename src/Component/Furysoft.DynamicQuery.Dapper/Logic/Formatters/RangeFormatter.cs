// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using DynamicQuery.Entities.Operations;
    using Entities;
    using Interfaces.Formatters;

    /// <summary>
    /// The Range Formatter
    /// </summary>
    public sealed class RangeFormatter : IWhereOperatorFormatter<RangeOperator>
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>The <see cref="SqlDataResponse"/></returns>
        public SqlDataResponse Format(RangeOperator node, IDictionary<string, object> dataDictionary)
        {
            var lowerOperator = node.LowerInclusive ? ">=" : ">";
            var upperOperator = node.UpperInclusive ? "<=" : "<";

            var sql = $"{node.Name} {lowerOperator} @{node.Name}Lower AND {node.Name} {upperOperator} @{node.Name}Upper";

            dataDictionary.Add($"{node.Name}Lower", node.Lower);
            dataDictionary.Add($"{node.Name}Upper", node.Upper);

            return new SqlDataResponse
            {
                Sql = sql,
                Params = dataDictionary
            };
        }
    }
}