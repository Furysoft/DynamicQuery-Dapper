// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormatterFactory.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic
{
    using System;
    using Furysoft.DynamicQuery.Dapper.Interfaces;
    using JetBrains.Annotations;

    /// <summary>
    /// The Formatter Factory.
    /// </summary>
    public sealed class FormatterFactory : IFormatterFactory
    {
        /// <summary>
        /// The count cte formatter.
        /// </summary>
        [NotNull]
        private readonly IFormatter countCteFormatter;

        /// <summary>
        /// The standard formatter.
        /// </summary>
        [NotNull]
        private readonly IFormatter standardFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatterFactory"/> class.
        /// </summary>
        /// <param name="standardFormatter">The standard formatter.</param>
        /// <param name="countCteFormatter">The count cte formatter.</param>
        public FormatterFactory([NotNull] IFormatter standardFormatter, [NotNull] IFormatter countCteFormatter)
        {
            this.standardFormatter = standardFormatter;
            this.countCteFormatter = countCteFormatter;
        }

        /// <summary>
        /// Creates the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The <see cref="IFormatter"/>.</returns>
        public IFormatter Create(FormatterType type)
        {
            switch (type)
            {
                case FormatterType.Standard:
                    return this.standardFormatter;
                case FormatterType.CountCte:
                    return this.countCteFormatter;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}