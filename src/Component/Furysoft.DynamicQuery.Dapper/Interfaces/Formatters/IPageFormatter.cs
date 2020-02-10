// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPageFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces.Formatters
{
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Entities.QueryComponents;

    /// <summary>
    /// The Page Formatter Interface.
    /// </summary>
    public interface IPageFormatter
    {
        /// <summary>
        /// Formats the specified page node.
        /// </summary>
        /// <param name="pageNode">The page node.</param>
        /// <returns>The <see cref="SqlDataResponse" />.</returns>
        SqlDataResponse Format(PageNode pageNode);
    }
}