// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountCteFormatterTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Integration.Formatters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Dapper.Logic;
    using Furysoft.DynamicQuery.Entities.QueryComponents;
    using Furysoft.DynamicQuery.Interfaces.QueryParsers;
    using Furysoft.DynamicQuery.Logic;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Count CTE Formatter Tests.
    /// </summary>
    [TestFixture]
    public sealed class CountCteFormatterTests : TestBase
    {
        /// <summary>
        /// Formats the when query in expect SQL formatted.
        /// </summary>
        [Test]
        public void Format_WhenQueryIn_ExpectSqlFormatted()
        {
            // Arrange
            var mockOrderByFormatter = new Mock<IOrderByFormatter>();
            var mockPageFormatter = new Mock<IPageFormatter>();
            var mockWhereFormatter = new Mock<IWhereFormatter>();
            var mockWhereParser = new Mock<IWhereParser>();
            var mockSelectParser = new Mock<ISelectParser>();

            mockWhereFormatter.Setup(r => r.Format(It.IsAny<WhereNode>())).Returns(new SqlDataResponse
            {
                Sql = "WHERE col1 = @col1",
            });

            mockOrderByFormatter.Setup(r => r.Format(It.IsAny<List<OrderByNode>>())).Returns("ORDER BY col1 asc");
            mockPageFormatter.Setup(r => r.Format(It.IsAny<PageNode>())).Returns(new SqlDataResponse
            {
                Sql = "OFFSET @offset LIMIT @limit",
            });

            mockSelectParser.Setup(r => r.Parse(It.IsAny<string>(), It.IsAny<char>())).Returns(new SelectNode
            {
                SelectAll = true,
                SelectColumns = new List<string> { "col_1", "col_2" },
            });

            var countCteFormatter = new CountCteFormatter(
                mockOrderByFormatter.Object,
                mockPageFormatter.Object,
                mockWhereFormatter.Object);

            var query = new Query(mockWhereParser.Object, mockSelectParser.Object);

            const string From = "table";

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlEntity = countCteFormatter.Format(query, From);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Console.WriteLine(sqlEntity.Sql);

            Assert.That(sqlEntity, Is.Not.Null);
        }
    }
}