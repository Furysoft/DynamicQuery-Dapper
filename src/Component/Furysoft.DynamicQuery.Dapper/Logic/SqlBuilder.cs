// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlBuilder.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic
{
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Interfaces;
    using Furysoft.DynamicQuery.Interfaces;
    using JetBrains.Annotations;

    /// <summary>
    /// The SQL Builder.
    /// </summary>
    public sealed class SqlBuilder : ISqlBuilder
    {
        /// <summary>
        /// The formatter factory.
        /// </summary>
        [NotNull]
        private readonly IFormatterFactory formatterFactory;

        /// <summary>
        /// The query.
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
        /// Gets or sets from query.
        /// </summary>
        private string FromQuery { get; set; }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The <see cref="SqlEntity"/>.</returns>
        public SqlEntity Build()
        {
            var formatterType = this.query.PageNode == null ? FormatterType.Standard : FormatterType.CountCte;
            var formatter = this.formatterFactory.Create(formatterType);

            return formatter.Format(this.query, this.FromQuery);
        }

        /// <summary>
        /// From the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>The <see cref="ISqlBuilder" />.</returns>
        public ISqlBuilder From(string from)
        {
            this.FromQuery = from;
            return this;
        }
    }
}