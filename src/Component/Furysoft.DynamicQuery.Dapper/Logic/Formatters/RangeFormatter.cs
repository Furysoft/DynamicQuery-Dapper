// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Entities.Operations;

    /// <summary>
    /// The Range Formatter.
    /// </summary>
    public sealed class RangeFormatter : IWhereOperatorFormatter<RangeOperator>
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="paramSuffix">The parameter suffix.</param>
        /// <returns>The <see cref="SqlDataResponse" />.</returns>
        public SqlDataResponse Format(RangeOperator node, int paramSuffix)
        {
            var lowerOperator = node.LowerInclusive ? ">=" : ">";
            var upperOperator = node.UpperInclusive ? "<=" : "<";

            var lowVarName = $"{node.Name}{paramSuffix}";
            var highVarName = $"{node.Name}{paramSuffix + 1}";

            var sql = $"{node.Name} {lowerOperator} @{lowVarName} AND {node.Name} {upperOperator} @{highVarName}";

            var param = new List<SqlWhereParam>
            {
                new SqlWhereParam { VarName = lowVarName, Value = node.Lower },
                new SqlWhereParam { VarName = highVarName, Value = node.Upper },
            };

            return new SqlDataResponse
            {
                Sql = sql,
                Params = param,
                LastSuffix = paramSuffix + 1,
            };
        }
    }
}