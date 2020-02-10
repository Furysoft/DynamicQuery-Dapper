// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualsFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using System.Text;
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Entities.Operations;

    /// <summary>
    /// The Equals Formatter.
    /// </summary>
    public sealed class EqualsFormatter : IWhereOperatorFormatter<EqualsOperator>
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="paramSuffix">The parameter suffix.</param>
        /// <returns>The <see cref="SqlDataResponse" />.</returns>
        public SqlDataResponse Format(EqualsOperator node, int paramSuffix)
        {
            /* Check for wildcards */
            var wildCardFormat = WildcardFormat(node, paramSuffix);
            if (wildCardFormat.IsWildcardQuery)
            {
                return new SqlDataResponse
                {
                    Sql = wildCardFormat.Sql,
                    Params = new List<SqlWhereParam> { wildCardFormat.Param },
                    LastSuffix = paramSuffix,
                };
            }

            /* Check for Null */
            var nullQueryFormat = NullQueryFormat(node);
            if (nullQueryFormat.IsNullQuery)
            {
                return new SqlDataResponse
                {
                    Sql = nullQueryFormat.Sql,
                    Params = new List<SqlWhereParam>(),
                    LastSuffix = paramSuffix,
                };
            }

            var op = GetOperator(node);

            var paramName = $"{node.Name}{paramSuffix}";

            var sql = $"{node.Name} {op} @{paramName}";

            var param = node.Value;
            if (param is string paramString)
            {
                param = paramString.Trim('"');
            }

            return new SqlDataResponse
            {
                Sql = sql,
                Params = new List<SqlWhereParam> { new SqlWhereParam { VarName = paramName, Value = param } },
                LastSuffix = paramSuffix,
            };
        }

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The Equality Operator.</returns>
        private static string GetOperator(EqualsOperator node)
        {
            return node.IsNot ? "<>" : "=";
        }

        /// <summary>
        /// Nulls the query format.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The null query sql.</returns>
        private static (bool IsNullQuery, string Sql) NullQueryFormat(EqualsOperator node)
        {
            if (node.Value as string != "NULL")
            {
                return (false, null);
            }

            return node.IsNot ? (true, $"{node.Name} IS NOT NULL") : (true, $"{node.Name} IS NULL");
        }

        /// <summary>
        /// Nulls the query format.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns>
        /// The LIKE query sql.
        /// </returns>
        private static (bool IsWildcardQuery, string Sql, SqlWhereParam Param) WildcardFormat(EqualsOperator node, int suffix)
        {
            if (!(node.Value is string nodeValue))
            {
                return (false, null, null);
            }

            var sb = new StringBuilder(nodeValue);
            var hasWildcard = false;

            if (nodeValue[0] == '*')
            {
                hasWildcard = true;
                sb.Replace('*', '%', 0, 1);
            }

            for (var index = 1; index < nodeValue.Length; index++)
            {
                if (nodeValue[index] == '*' && nodeValue[index - 1] != '\\')
                {
                    hasWildcard = true;
                    sb.Replace('*', '%', index, 1);
                }
            }

            if (hasWildcard)
            {
                var varName = $"{node.Name}{suffix}";

                sb.Replace("\\*", "*");
                var dataPart = sb.ToString();

                var sql = node.IsNot
                    ? $"{node.Name} NOT LIKE @{varName}"
                    : $"{node.Name} LIKE @{varName}";

                return (true, sql, new SqlWhereParam { Value = dataPart, VarName = varName });
            }

            return (false, null, null);
        }
    }
}