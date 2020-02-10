// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFormatterFactory.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Interfaces
{
    /// <summary>
    /// The Formatter Factory Interface.
    /// </summary>
    public interface IFormatterFactory
    {
        /// <summary>
        /// Creates the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="IFormatter"/>.</returns>
        IFormatter Create(FormatterType type);
    }
}