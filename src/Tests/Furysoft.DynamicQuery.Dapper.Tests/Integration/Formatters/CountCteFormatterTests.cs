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
    using DynamicQuery.Entities.Nodes;
    using DynamicQuery.Entities.Operations;
    using DynamicQuery.Entities.QueryComponents;
    using DynamicQuery.Logic;
    using Entities;
    using Interfaces.Formatters;
    using Logic;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Count CTE Formatter Tests
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

            mockWhereFormatter.Setup(r => r.Format(It.IsAny<Node>(), It.IsAny<IDictionary<string, object>>())).Returns(new SqlDataResponse
            {
                Sql = "WHERE col1 = @col1"
            });

            mockOrderByFormatter.Setup(r => r.Format(It.IsAny<List<OrderByNode>>())).Returns("ORDER BY col1 asc");
            mockPageFormatter.Setup(r => r.Format(It.IsAny<PageNode>(), It.IsAny<IDictionary<string, object>>())).Returns(new SqlDataResponse
            {
                Sql = "OFFSET @offset LIMIT @limit"
            });

            var countCteFormatter = new CountCteFormatter(
                mockOrderByFormatter.Object,
                mockPageFormatter.Object,
                mockWhereFormatter.Object);

            var orderByNodes = new List<OrderByNode> { new OrderByNode() };
            var node = new EqualsOperator();
            var pageNode = new PageNode();

            var query = new Query(orderByNodes, node, pageNode);

            const string Select = "col1, col2";
            const string From = "table";

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlEntity = countCteFormatter.Format(query, Select, From);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Console.WriteLine(sqlEntity.Sql);

            Assert.That(sqlEntity, Is.Not.Null);
        }
    }
}