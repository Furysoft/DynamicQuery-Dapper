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
        /// The formatter factory
        /// </summary>
        [NotNull]
        private readonly IFormatterFactory formatterFactory;

        /// <summary>
        /// The query
        /// </summary>
        [NotNull]
        private readonly IQuery query;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlBuilder" /> class.
        /// </summary>
        /// <param name="formatterFactory">The formatter factory.</param>
        /// <param name="query">The query.</param>
        public SqlBuilder(
            [NotNull] IFormatterFactory formatterFactory,
            [NotNull] IQuery query)
        {
            this.query = query;
            this.formatterFactory = formatterFactory;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [count cte].
        /// </summary>
        private bool CountCte { get; set; } = false;

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
            var type = this.CountCte ? FormatterType.CountCte : FormatterType.Standard;
            var formatter = this.formatterFactory.Create(type);

            return formatter.Format(this.query, this.SelectQuery, this.FromQuery);
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

        /// <summary>
        /// Withes the count cte.
        /// </summary>
        /// <returns>The <see cref="ISqlBuilder"/></returns>
        public ISqlBuilder WithCountCte()
        {
            this.CountCte = true;
            return this;
        }
    }
}