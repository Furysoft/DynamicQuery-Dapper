// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormatterFactory.cs" company="Email Hippo Ltd">
//   © Email Hippo Ltd
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
    public static class FormatterFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The <see cref="IFormatter"/></returns>
        public static IFormatter Create()
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

            return new Formatter(orderByFormatter, pageFormatter, whereFormatter);
        }
    }
}