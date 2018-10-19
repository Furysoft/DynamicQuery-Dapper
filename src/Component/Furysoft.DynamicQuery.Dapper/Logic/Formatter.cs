// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Formatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DynamicQuery.Interfaces;
    using Entities;
    using global::Dapper;
    using Interfaces;
    using Interfaces.Formatters;
    using JetBrains.Annotations;

    /// <summary>
    /// The Formatter
    /// </summary>
    public sealed class Formatter : IFormatter
    {
        /// <summary>
        /// The order by formatter
        /// </summary>
        [NotNull]
        private readonly IOrderByFormatter orderByFormatter;

        /// <summary>
        /// The page formatter
        /// </summary>
        [NotNull]
        private readonly IPageFormatter pageFormatter;

        /// <summary>
        /// The where formatter
        /// </summary>
        [NotNull]
        private readonly IWhereFormatter whereFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Formatter"/> class.
        /// </summary>
        /// <param name="orderByFormatter">The order by formatter.</param>
        /// <param name="pageFormatter">The page formatter.</param>
        /// <param name="whereFormatter">The where formatter.</param>
        public Formatter(
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
        /// <param name="selectQuery">The select query.</param>
        /// <param name="fromQuery">From query.</param>
        /// <returns>
        /// The <see cref="SqlEntity" />
        /// </returns>
        public SqlEntity Format(IQuery query, string selectQuery, string fromQuery)
        {
            var dataDictionary = new Dictionary<string, object>();

            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(selectQuery))
            {
                sb.AppendFormat("SELECT {0}\r\n", selectQuery);
            }

            if (!string.IsNullOrWhiteSpace(fromQuery))
            {
                sb.AppendFormat("FROM {0}\r\n", fromQuery);
            }

            if (query.WhereNode != null)
            {
                var where = this.whereFormatter.Format(query.WhereNode, dataDictionary);
                sb.AppendFormat("{0}\r\n", where.Sql);
            }

            if (query.OrderByNodes != null && query.OrderByNodes.Any())
            {
                var orderBy = this.orderByFormatter.Format(query.OrderByNodes);
                sb.AppendFormat("{0}\r\n", orderBy);
            }

            if (query.PageNode != null)
            {
                var page = this.pageFormatter.Format(query.PageNode, dataDictionary);
                sb.AppendFormat("{0}\r\n", page.Sql);
            }

            var sql = sb.ToString();

            var data = new DynamicParameters();
            foreach (var keyValue in dataDictionary)
            {
                data.Add(keyValue.Key, keyValue.Value);
            }

            return new SqlEntity(sql, data);
        }
    }
}