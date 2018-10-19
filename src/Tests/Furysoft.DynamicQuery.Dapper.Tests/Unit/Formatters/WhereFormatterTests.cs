// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereFormatterTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Unit.Formatters
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using DynamicQuery.Entities.Operations;
    using Entities;
    using Interfaces.Formatters;
    using Logic.Formatters;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Where Formatter Tests
    /// </summary>
    [TestFixture]
    public sealed class WhereFormatterTests : TestBase
    {
        /// <summary>
        /// Formats the when single node expect where statement.
        /// </summary>
        [Test]
        public void Format_WhenSingleNode_ExpectWhereStatement()
        {
            // Arrange
            var mockEqualsFormatter = new Mock<IWhereOperatorFormatter<EqualsOperator>>();
            var mockLessThanFormatter = new Mock<IWhereOperatorFormatter<LessThanOperator>>();
            var mockGreaterThanFormatter = new Mock<IWhereOperatorFormatter<GreaterThanOperator>>();
            var mockRangeFormatter = new Mock<IWhereOperatorFormatter<RangeOperator>>();

            mockEqualsFormatter
                .Setup(r => r.Format(It.IsAny<EqualsOperator>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(new SqlDataResponse
                {
                    Sql = "ColumnName = @ColumnName",
                });

            var whereFormatter = new WhereFormatter(
                mockEqualsFormatter.Object,
                mockLessThanFormatter.Object,
                mockGreaterThanFormatter.Object,
                mockRangeFormatter.Object);

            var whereNode = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Statement = "ColumnName:bob",
                Value = "bob"
            };

            var dataDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = whereFormatter.Format(whereNode, dataDictionary);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse, Is.Not.Null);

        // Assert.That(sqlDataResponse.Sql, Is.EqualTo("WHERE ColumnName = @ColumnName"));
        // Assert.That(sqlDataResponse.Params["ColumnName"], Is.EqualTo("bob"));
        }
    }
}