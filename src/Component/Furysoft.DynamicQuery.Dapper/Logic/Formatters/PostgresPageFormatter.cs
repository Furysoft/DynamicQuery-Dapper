// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostgresPageFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using DynamicQuery.Entities.QueryComponents;
    using Entities;
    using Interfaces.Formatters;
    using Paging;

    /// <summary>
    /// The Postgres Page Formatter
    /// </summary>
    public sealed class PostgresPageFormatter : IPageFormatter
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>
        /// The formatted sql string
        /// </returns>
        public SqlDataResponse Format(PageNode node, IDictionary<string, object> dataDictionary)
        {
            if (node == null)
            {
                return null;
            }

            const string Sql = "OFFSET @offset LIMIT @limit";

            var skip = PagingHelpers.GetSkip(node.Page, node.ItemsPerPage);

            dataDictionary.Add("offset", skip);
            dataDictionary.Add("limit", node.ItemsPerPage);

            return new SqlDataResponse
            {
                Sql = Sql,
                Params = dataDictionary
            };
        }
    }
}