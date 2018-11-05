// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormatterInitializer.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper
{
    using Interfaces;
    using Logic;
    using Logic.Formatters;

    /// <summary>
    /// The Formatter Factory
    /// </summary>
    public static class FormatterInitializer
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The <see cref="IFormatter"/></returns>
        public static IFormatterFactory Create()
        {
            var orderByFormatter = new OrderByFormatter();
            var pageFormatter = new PostgresPageFormatter();

            var equalsFormatter = new EqualsFormatter();
            var lessThanFormatter = new LessThanFormatter();
            var greaterThanFormatter = new GreaterThanFormatter();
            var rangeFormatter = new RangeFormatter();

            var whereFormatter = new WhereFormatter(
                equalsFormatter,
                lessThanFormatter,
                greaterThanFormatter,
                rangeFormatter);

            var standardFormatter = new Formatter(orderByFormatter, pageFormatter, whereFormatter);
            var countCteFormatter = new CountCteFormatter(orderByFormatter, pageFormatter, whereFormatter);

            return new FormatterFactory(standardFormatter, countCteFormatter);
        }
    }
}