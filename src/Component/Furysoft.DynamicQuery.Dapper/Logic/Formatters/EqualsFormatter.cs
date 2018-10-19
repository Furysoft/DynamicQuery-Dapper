// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualsFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using System.Text;
    using DynamicQuery.Entities.Operations;
    using Entities;
    using Interfaces.Formatters;

    /// <summary>
    /// The Equals Formatter
    /// </summary>
    public sealed class EqualsFormatter : IWhereOperatorFormatter<EqualsOperator>
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>The <see cref="SqlDataResponse"/></returns>
        public SqlDataResponse Format(EqualsOperator node, IDictionary<string, object> dataDictionary)
        {
            if (node == null)
            {
                return null;
            }

            /* Check for wildcards */
            var wildCardFormat = WildcardFormat(node, dataDictionary);
            if (wildCardFormat.IsWildcardQuery)
            {
                return new SqlDataResponse
                {
                    Sql = wildCardFormat.Sql,
                    Params = dataDictionary
                };
            }

            /* Check for Null */
            var nullQueryFormat = NullQueryFormat(node);
            if (nullQueryFormat.IsNullQuery)
            {
                return new SqlDataResponse
                {
                    Sql = nullQueryFormat.Sql,
                    Params = dataDictionary
                };
            }

            var op = GetOperator(node);

            var sql = $"{node.Name} {op} @{node.Name}";

            var param = node.Value;

            if (param is string paramString)
            {
                param = paramString.Trim('"');
            }

            dataDictionary.Add(node.Name, param);

            return new SqlDataResponse
            {
                Sql = sql,
                Params = dataDictionary
            };
        }

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The Equality Operator</returns>
        private static string GetOperator(EqualsOperator node)
        {
            return node.IsNot ? "<>" : "=";
        }

        /// <summary>
        /// Nulls the query format.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The null query sql</returns>
        private static(bool IsNullQuery, string Sql) NullQueryFormat(EqualsOperator node)
        {
            if (node.Value as string == "NULL")
            {
                if (node.IsNot)
                {
                    return (true, $"{node.Name} IS NOT NULL");
                }

                return (true, $"{node.Name} IS NULL");
            }

            return (false, null);
        }

        /// <summary>
        /// Nulls the query format.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>
        /// The LIKE query sql
        /// </returns>
        private static(bool IsWildcardQuery, string Sql) WildcardFormat(
            EqualsOperator node,
            IDictionary<string, object> dataDictionary)
        {
            var nodeValue = node.Value as string;
            if (nodeValue == null)
            {
                return (false, null);
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
                sb.Replace("\\*", "*");
                var dataPart = sb.ToString();

                var sql = node.IsNot
                    ? $"{node.Name} NOT LIKE @{node.Name}"
                    : $"{node.Name} LIKE @{node.Name}";

                dataDictionary.Add(node.Name, dataPart);

                return (true, sql);
            }

            return (false, null);
        }
    }
}