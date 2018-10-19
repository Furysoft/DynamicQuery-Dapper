// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISqlBuilder.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces
{
    using DynamicQuery.Entities;
    using Entities;

    /// <summary>
    /// The SQL Builder Interface
    /// </summary>
    public interface ISqlBuilder
    {
        /// <summary>
        /// Selects the specified select.
        /// </summary>
        /// <param name="select">The select.</param>
        /// <returns>The <see cref="ISqlBuilder"/></returns>
        ISqlBuilder Select(string select);

        /// <summary>
        /// From the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>The <see cref="ISqlBuilder"/></returns>
        ISqlBuilder From(string from);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The <see cref="SqlEntity"/></returns>
        SqlEntity Build();
    }
}