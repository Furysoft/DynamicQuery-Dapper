// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWhereFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces.Formatters
{
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Entities.QueryComponents;

    /// <summary>
    /// The Where Formatter Interface.
    /// </summary>
    public interface IWhereFormatter
    {
        /// <summary>
        /// Formats the specified where node.
        /// </summary>
        /// <param name="whereNode">The where node.</param>
        /// <returns>The <see cref="SqlDataResponse" />.</returns>
        SqlDataResponse Format(WhereNode whereNode);
    }
}