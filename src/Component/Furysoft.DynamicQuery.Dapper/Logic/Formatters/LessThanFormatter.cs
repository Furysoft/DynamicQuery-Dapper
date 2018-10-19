// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LessThanFormatter.cs" company="Simon Paramore">
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
    /// The Less Than Formatter
    /// </summary>
    /// <seealso cref="IWhereOperatorFormatter{LessThanOperator}" />
    public sealed class LessThanFormatter : IWhereOperatorFormatter<LessThanOperator>
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>The <see cref="SqlDataResponse"/></returns>
        public SqlDataResponse Format(LessThanOperator node, IDictionary<string, object> dataDictionary)
        {
            if (node == null)
            {
                return null;
            }

            dataDictionary.Add(node.Name, node.Value);

            var sql = node.Inclusive
                ? $"{node.Name} <= @{node.Name}"
                : $"{node.Name} < @{node.Name}";

            var rtn = new SqlDataResponse
            {
                Params = dataDictionary,
                Sql = sql
            };

            return rtn;
        }
    }
}