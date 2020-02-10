// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereFormatterTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Unit.Formatters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Dapper.Logic.Formatters;
    using Furysoft.DynamicQuery.Entities.Operations;
    using Furysoft.DynamicQuery.Entities.QueryComponents;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Where Formatter Tests.
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
                .Setup(r => r.Format(It.IsAny<EqualsOperator>(), It.IsAny<int>()))
                .Returns(new SqlDataResponse
                {
                    Sql = "ColumnName = @ColumnName1",
                    LastSuffix = 1,
                    Params = new List<SqlWhereParam> { new SqlWhereParam { Value = "bob", VarName = "ColumnName1" } },
                });

            var whereFormatter = new WhereFormatter(
                mockEqualsFormatter.Object,
                mockLessThanFormatter.Object,
                mockGreaterThanFormatter.Object,
                mockRangeFormatter.Object);

            var op = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Statement = "ColumnName:bob",
                Value = "bob",
            };

            var whereNode = new WhereNode
            {
                Statement = new WhereStatement { Value = op },
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = whereFormatter.Format(whereNode);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse, Is.Not.Null);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo($"WHERE ColumnName = @ColumnName1{Environment.NewLine}"));
            Assert.That(sqlDataResponse.Params.First(r => r.VarName == "ColumnName1").Value, Is.EqualTo("bob"));
        }
    }
}