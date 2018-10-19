// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlBuilder.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic
{
    using DynamicQuery.Interfaces;
    using Entities;
    using Interfaces;
    using JetBrains.Annotations;

    /// <summary>
    /// The SQL Builder
    /// </summary>
    public sealed class SqlBuilder : ISqlBuilder
    {
        /// <summary>
        /// The formatter
        /// </summary>
        [NotNull]
        private readonly IFormatter formatter;

        /// <summary>
        /// The query
        /// </summary>
        [NotNull]
        private readonly IQuery query;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlBuilder" /> class.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="formatter">The formatter.</param>
        public SqlBuilder(
            [NotNull] IFormatter formatter,
            [NotNull] IQuery query)
        {
            this.query = query;
            this.formatter = formatter;
        }

        /// <summary>
        /// Gets or sets from query.
        /// </summary>
        private string FromQuery { get; set; }

        /// <summary>
        /// Gets or sets the select query.
        /// </summary>
        private string SelectQuery { get; set; }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The <see cref="SqlEntity"/></returns>
        public SqlEntity Build()
        {
            return this.formatter.Format(this.query, this.SelectQuery, this.FromQuery);
        }

        /// <summary>
        /// From the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>The <see cref="ISqlBuilder" /></returns>
        public ISqlBuilder From(string from)
        {
            this.FromQuery = from;
            return this;
        }

        /// <summary>
        /// Selects the specified select.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <returns>The <see cref="ISqlBuilder"/></returns>
        public ISqlBuilder Select(string select)
        {
            this.SelectQuery = select;
            return this;
        }
    }
}