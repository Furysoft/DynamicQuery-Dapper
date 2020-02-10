// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicQueryExtensions.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper
{
    using Furysoft.DynamicQuery.Dapper.Interfaces;
    using Furysoft.DynamicQuery.Dapper.Logic;
    using Furysoft.DynamicQuery.Interfaces;

    /// <summary>
    /// The DynamicQueryExtensions.
    /// </summary>
    public static class DynamicQueryExtensions
    {
        /// <summary>
        /// The formatter.
        /// </summary>
        private static readonly IFormatterFactory Formatter = FormatterInitializer.Create();

        /// <summary>
        /// Builds the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The <see cref="ISqlBuilder"/>.</returns>
        public static ISqlBuilder CreateSqlQuery(this IQuery query)
        {
            return new SqlBuilder(Formatter, query);
        }
    }
}