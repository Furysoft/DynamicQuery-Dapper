// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces
{
    using DynamicQuery.Interfaces;
    using Entities;

    /// <summary>
    /// The Formatter Interface
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Formats the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="select">The select.</param>
        /// <param name="from">From.</param>
        /// <returns>
        /// The <see cref="SqlEntity" />
        /// </returns>
        SqlEntity Format(IQuery query, string select, string from);
    }
}