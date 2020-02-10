// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GreaterThanFormatter.cs" company="Simon Paramore">
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
    /// The Greater Than Formatter.
    /// </summary>
    /// <seealso cref="IWhereOperatorFormatter{LessThanOperator}" />
    public sealed class GreaterThanFormatter : IWhereOperatorFormatter<GreaterThanOperator>
    {
        /// <inheritdoc />
        public SqlDataResponse Format(GreaterThanOperator node, int paramSuffix)
        {
            var varName = $"{node.Name}{paramSuffix}";

            var sql = node.Inclusive
                ? $"{node.Name} >= @{varName}"
                : $"{node.Name} > @{varName}";

            return new SqlDataResponse
            {
                Params = new List<SqlWhereParam> { new SqlWhereParam { Value = node.Value, VarName = varName } },
                Sql = sql,
                LastSuffix = paramSuffix,
            };
        }
    }
}