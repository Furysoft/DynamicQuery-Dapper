// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LessThanFormatter.cs" company="Simon Paramore">
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
    /// The Less Than Formatter.
    /// </summary>
    /// <seealso cref="IWhereOperatorFormatter{LessThanOperator}" />
    public sealed class LessThanFormatter : IWhereOperatorFormatter<LessThanOperator>
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="paramSuffix">The parameter suffix.</param>
        /// <returns>The <see cref="SqlDataResponse" />.</returns>
        public SqlDataResponse Format(LessThanOperator node, int paramSuffix)
        {
            var varName = $"{node.Name}{paramSuffix}";

            var sql = node.Inclusive
                ? $"{node.Name} <= @{varName}"
                : $"{node.Name} < @{varName}";

            return new SqlDataResponse
            {
                Params = new List<SqlWhereParam> { new SqlWhereParam { Value = node.Value, VarName = varName } },
                Sql = sql,
                LastSuffix = paramSuffix,
            };
        }
    }
}