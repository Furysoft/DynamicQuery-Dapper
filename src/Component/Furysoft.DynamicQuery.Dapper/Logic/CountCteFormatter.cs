// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountCteFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Exceptions;
    using Furysoft.DynamicQuery.Dapper.Interfaces;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Interfaces;
    using global::Dapper;
    using JetBrains.Annotations;

    /// <summary>
    /// The Count CTR Formatter.
    /// </summary>
    public sealed class CountCteFormatter : IFormatter
    {
        /// <summary>
        /// The order by formatter.
        /// </summary>
        [NotNull]
        private readonly IOrderByFormatter orderByFormatter;

        /// <summary>
        /// The page formatter.
        /// </summary>
        [NotNull]
        private readonly IPageFormatter pageFormatter;

        /// <summary>
        /// The where formatter.
        /// </summary>
        [NotNull]
        private readonly IWhereFormatter whereFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountCteFormatter"/> class.
        /// </summary>
        /// <param name="orderByFormatter">The order by formatter.</param>
        /// <param name="pageFormatter">The page formatter.</param>
        /// <param name="whereFormatter">The where formatter.</param>
        public CountCteFormatter(
            [NotNull] IOrderByFormatter orderByFormatter,
            [NotNull] IPageFormatter pageFormatter,
            [NotNull] IWhereFormatter whereFormatter)
        {
            this.orderByFormatter = orderByFormatter;
            this.pageFormatter = pageFormatter;
            this.whereFormatter = whereFormatter;
        }

        /// <summary>
        /// Formats the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="fromQuery">From query.</param>
        /// <returns>The <see cref="SqlEntity" />.</returns>
        public SqlEntity Format(IQuery query, string fromQuery)
        {
            var sb = new StringBuilder();

            /* The data_cte section */
            sb.Append("WITH data_cte AS (\r\n");

            if (query.SelectNode != null)
            {
                var select = string.Join(", ", query.SelectNode.SelectColumns);
                sb.AppendFormat(" SELECT {0}\r\n", select);
            }
            else
            {
                // If no select statement is provided, select everything.
                var newSelectQuery = query.SelectAll();
                var select = string.Join(", ", newSelectQuery.SelectNode.SelectColumns);
                sb.AppendFormat(" SELECT {0}\r\n", select);
            }

            if (!string.IsNullOrWhiteSpace(fromQuery))
            {
                sb.AppendFormat(" FROM {0}\r\n", fromQuery);
            }
            else
            {
                throw new SqlParseException("No 'from' statement was provided for SQL type query.");
            }

            var whereParams = new List<SqlWhereParam>();
            if (query.WhereNode != null)
            {
                var where = this.whereFormatter.Format(query.WhereNode);
                sb.AppendFormat(" {0}\r\n", where.Sql);
                whereParams = where.Params;
            }

            /* The count section */
            sb.Append("), count_cte AS (SELECT COUNT(*) as total_rows FROM data_cte)\r\n");

            /* The Cross Join section */
            sb.AppendFormat(" SELECT {0}, total_rows\r\n", query.SelectNode.SelectColumns);

            sb.AppendFormat("FROM data_cte CROSS JOIN count_cte\r\n");

            if (query.OrderByNodes != null && query.OrderByNodes.Any())
            {
                var orderBy = this.orderByFormatter.Format(query.OrderByNodes);
                sb.AppendFormat("{0}\r\n", orderBy);
            }

            if (query.PageNode != null)
            {
                var page = this.pageFormatter.Format(query.PageNode);
                sb.AppendFormat("{0}\r\n", page.Sql);
                whereParams = whereParams.Concat(page.Params).ToList();
            }

            var sql = sb.ToString();

            var data = new DynamicParameters();
            foreach (var whereParam in whereParams)
            {
                data.Add(whereParam.VarName, whereParam.Value);
            }

            return new SqlEntity(sql, data);
        }
    }
}