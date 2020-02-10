// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualsFormatterTests.cs" company="Simon Paramore">
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
    /// The Equals Formatter Tests.
    /// </summary>
    [TestFixture]
    public sealed class EqualsFormatterTests : TestBase
    {
        /// <summary>
        /// Formats the when is wildcard prefix expect like SQL.
        /// </summary>
        [Test]
        public void Format_WhenIsWildcardPrefix_ExpectLikeSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Value = "*bob",
                Statement = "ColumnName:*bob",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName LIKE @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo("%bob"));
        }

        /// <summary>
        /// Formats the when is wildcard prefix and suffix expect like SQL.
        /// </summary>
        [Test]
        public void Format_WhenIsWildcardPrefixAndSuffix_ExpectLikeSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Value = "*bob*",
                Statement = "ColumnName:*bob*",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName LIKE @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo("%bob%"));
        }

        /// <summary>
        /// Formats the when is wildcard suffix expect like SQL.
        /// </summary>
        [Test]
        public void Format_WhenIsWildcardSuffix_ExpectLikeSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Value = "bob*",
                Statement = "ColumnName:bob*",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName LIKE @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo("bob%"));
        }

        /// <summary>
        /// Formats the when multiple wildcards expect like SQL.
        /// </summary>
        [Test]
        public void Format_WhenMultipleWildcards_ExpectLikeSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Value = @"*b*o\*b*",
                Statement = @"ColumnName:*b*o\*b*",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName LIKE @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo(@"%b%o*b%"));
        }

        /// <summary>
        /// Formats the when search for not null expect is not null SQL.
        /// </summary>
        [Test]
        public void Format_WhenSearchForNotNull_ExpectIsNotNullSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = true,
                Value = @"NULL",
                Statement = @"ColumnName:NULL",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName IS NOT NULL"));
            Assert.That(sqlDataResponse.Params.Any(), Is.False);
        }

        /// <summary>
        /// Formats the when search for null expect is null SQL.
        /// </summary>
        [Test]
        public void Format_WhenSearchForNull_ExpectIsNullSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Value = @"NULL",
                Statement = @"ColumnName:NULL",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName IS NULL"));
            Assert.That(sqlDataResponse.Params.Any(), Is.False);
        }

        /// <summary>
        /// Formats the when search for word null expect standard equals.
        /// </summary>
        [Test]
        public void Format_WhenSearchForWordNull_ExpectStandardEquals()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Value = "\"NULL\"",
                Statement = "ColumnName:\"NULL\"",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName = @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo(@"NULL"));
        }

        /// <summary>
        /// Formats the when string comparison expect correct SQL.
        /// </summary>
        [Test]
        public void Format_WhenStringComparison_ExpectCorrectSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Value = "bob",
                Statement = "ColumnName:bob",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName = @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo("bob"));
        }

        /// <summary>
        /// Formats the when string not comparison expect correct SQL.
        /// </summary>
        [Test]
        public void Format_WhenStringNotComparison_ExpectCorrectSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = true,
                Value = "bob",
                Statement = "ColumnName:bob",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName <> @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo("bob"));
        }

        /// <summary>
        /// Formats the when wildcard with not expect not like SQL.
        /// </summary>
        [Test]
        public void Format_WhenWildcardWithNot_ExpectNotLikeSql()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();

            var node = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = true,
                Value = @"*bob",
                Statement = @"ColumnName:*bob",
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = equalsFormatter.Format(node, 0);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName NOT LIKE @ColumnName0"));
            Assert.That(sqlDataResponse.Params.First().Value, Is.EqualTo(@"%bob"));
        }
    }
}