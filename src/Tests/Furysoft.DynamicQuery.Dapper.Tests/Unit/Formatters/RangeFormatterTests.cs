// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeFormatterTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Unit.Formatters
{
    using System.Diagnostics;
    using System.Linq;
    using Furysoft.DynamicQuery.Dapper.Logic.Formatters;
    using Furysoft.DynamicQuery.Entities.Operations;
    using NUnit.Framework;

    /// <summary>
    /// The Range Formatter Tests.
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
                UpperInclusive = false,
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = rangeFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName >= @ColumnName0 AND ColumnName < @ColumnName1"));
            Assert.That(sqlDataResponse.Params.First(r => r.VarName == "ColumnName0").Value, Is.EqualTo(25));
            Assert.That(sqlDataResponse.Params.First(r => r.VarName == "ColumnName1").Value, Is.EqualTo(100));
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
                UpperInclusive = true,
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = rangeFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName > @ColumnName0 AND ColumnName <= @ColumnName1"));
            Assert.That(sqlDataResponse.Params.First(r => r.VarName == "ColumnName0").Value, Is.EqualTo(25));
            Assert.That(sqlDataResponse.Params.First(r => r.VarName == "ColumnName1").Value, Is.EqualTo(100));
        }
    }
}