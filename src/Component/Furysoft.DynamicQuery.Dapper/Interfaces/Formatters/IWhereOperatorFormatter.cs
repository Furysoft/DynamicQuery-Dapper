// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWhereOperatorFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces.Formatters
{
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Entities.Nodes;
    using JetBrains.Annotations;

    /// <summary>
    /// The Where Operator Formatter.
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    public interface IWhereOperatorFormatter<in TType>
        where TType : UnaryNode
    {
        /// <summary>
        /// Formats the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="paramSuffix">The parameter suffix.</param>
        /// <returns>The <see cref="SqlDataResponse" />.</returns>
        [NotNull]
        SqlDataResponse Format([NotNull] TType node, int paramSuffix);
    }
}