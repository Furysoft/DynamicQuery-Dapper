// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderByFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Entities;
    using Furysoft.DynamicQuery.Entities.QueryComponents;

    /// <summary>
    /// The Order By Formatter.
    /// </summary>
    public sealed class OrderByFormatter : IOrderByFormatter
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The formatted sql string.</returns>
        public string Format(List<OrderByNode> node)
        {
            if (node == null || !node.Any())
            {
                return null;
            }

            var sb = new StringBuilder();

            sb.Append("ORDER BY ");

            foreach (var orderByNode in node)
            {
                var sortOrder = GetSortOrder(orderByNode.SortOrder);
                sb.AppendFormat("{0} {1},", orderByNode.Name, sortOrder);
            }

            sb.Length--;

            return sb.ToString();
        }

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>The Sort Order.</returns>
        private static string GetSortOrder(SortOrder order)
        {
            switch (order)
            {
                case SortOrder.Asc:
                    return "ASC";
                case SortOrder.Desc:
                    return "DESC";
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }
        }
    }
}