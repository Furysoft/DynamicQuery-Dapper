// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderByFormatterTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Unit.Formatters
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Furysoft.DynamicQuery.Dapper.Logic.Formatters;
    using Furysoft.DynamicQuery.Entities;
    using Furysoft.DynamicQuery.Entities.QueryComponents;
    using NUnit.Framework;

    /// <summary>
    /// The Order By Formatter Tests.
    /// </summary>
    [TestFixture]
    public sealed class OrderByFormatterTests : TestBase
    {
        /// <summary>
        /// Formats the when single order by expect order by returned.
        /// </summary>
        [Test]
        public void Format_WhenSingleOrderBy_ExpectOrderByReturned()
        {
            // Arrange
            var orderByFormatter = new OrderByFormatter();

            var nodes = new List<OrderByNode>
            {
                new OrderByNode
                {
                    SortOrder = SortOrder.Asc,
                    Name = "col1",
                },
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var format = orderByFormatter.Format(nodes);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(format, Is.EqualTo("ORDER BY col1 ASC"));
        }

        /// <summary>
        /// Formats the when empty order by expect nul l returned.
        /// </summary>
        [Test]
        public void Format_WhenEmptyOrderBy_ExpectNulLReturned()
        {
            // Arrange
            var orderByFormatter = new OrderByFormatter();

            var nodes = new List<OrderByNode>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var format = orderByFormatter.Format(nodes);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(format, Is.Null);
        }

        /// <summary>
        /// Formats the when multiple order by expect order by returned with all columns.
        /// </summary>
        [Test]
        public void Format_WhenMultipleOrderBy_ExpectOrderByReturnedWithAllColumns()
        {
            // Arrange
            var orderByFormatter = new OrderByFormatter();

            var nodes = new List<OrderByNode>
            {
                new OrderByNode
                {
                    SortOrder = SortOrder.Asc,
                    Name = "col1",
                },
                new OrderByNode
                {
                    SortOrder = SortOrder.Desc,
                    Name = "col2",
                },
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var format = orderByFormatter.Format(nodes);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(format, Is.EqualTo("ORDER BY col1 ASC,col2 DESC"));
        }
    }
}