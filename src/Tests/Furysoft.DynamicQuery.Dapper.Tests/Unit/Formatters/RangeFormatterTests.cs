// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeFormatterTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Unit.Formatters
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using DynamicQuery.Entities.Operations;
    using Logic.Formatters;
    using NUnit.Framework;

    /// <summary>
    /// The Range Formatter Tests
    /// </summary>
    [TestFixture]
    public sealed class RangeFormatterTests : TestBase
    {
        /// <summary>
        /// Formats the when lower inclusive expect correct SQL.
        /// </summary>
        [Test]
        public void Format_WhenLowerInclusive_ExpectCorrectSql()
        {
            // Arrange
            var rangeFormatter = new RangeFormatter();

            var node = new RangeOperator
            {
                Name = "ColumnName",
                Lower = 25,
                Upper = 100,
                LowerInclusive = true,
                UpperInclusive = false
            };

            var dataDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = rangeFormatter.Format(node, dataDictionary);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName >= @ColumnNameLower AND ColumnName < @ColumnNameUpper"));
            Assert.That(sqlDataResponse.Params["ColumnNameLower"], Is.EqualTo(25));
            Assert.That(sqlDataResponse.Params["ColumnNameUpper"], Is.EqualTo(100));
        }

        /// <summary>
        /// Formats the when upper inclusive expect correct SQL.
        /// </summary>
        [Test]
        public void Format_WhenUpperInclusive_ExpectCorrectSql()
        {
            // Arrange
            var rangeFormatter = new RangeFormatter();

            var node = new RangeOperator
            {
                Name = "ColumnName",
                Lower = 25,
                Upper = 100,
                LowerInclusive = false,
                UpperInclusive = true
            };

            var dataDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = rangeFormatter.Format(node, dataDictionary);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName > @ColumnNameLower AND ColumnName <= @ColumnNameUpper"));
            Assert.That(sqlDataResponse.Params["ColumnNameLower"], Is.EqualTo(25));
            Assert.That(sqlDataResponse.Params["ColumnNameUpper"], Is.EqualTo(100));
        }
    }
}