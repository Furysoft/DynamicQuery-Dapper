// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostgresPageFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Entities.QueryComponents;
    using Furysoft.Paging;

    /// <summary>
    /// The Postgres Page Formatter.
    /// </summary>
    public sealed class PostgresPageFormatter : IPageFormatter
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The formatted sql string.</returns>
        public SqlDataResponse Format(PageNode node)
        {
            if (node == null)
            {
                return null;
            }

            const string Sql = "OFFSET @offset LIMIT @limit";

            var skip = PagingHelpers.GetSkip(node.Page, node.ItemsPerPage);

            return new SqlDataResponse
            {
                Sql = Sql,
                Params = new List<SqlWhereParam>
                {
                    new SqlWhereParam { VarName = "offset", Value = skip },
                    new SqlWhereParam { VarName = "limit", Value = node.ItemsPerPage },
                },
            };
        }
    }
}