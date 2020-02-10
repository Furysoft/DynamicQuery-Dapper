// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereFormatterTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Integration.Formatters
{
    using System;
    using System.Diagnostics;
    using Furysoft.DynamicQuery.Dapper.Logic.Formatters;
    using Furysoft.DynamicQuery.Entities.Nodes;
    using Furysoft.DynamicQuery.Entities.Operations;
    using Furysoft.DynamicQuery.Entities.QueryComponents;
    using NUnit.Framework;

    /// <summary>
    /// The Where Formatter Tests.
    /// </summary>
    [TestFixture]
    public sealed class WhereFormatterTests : TestBase
    {
        /// <summary>
        /// Formats this instance.
        /// </summary>
        [Test]
        public void Format_WhenBinaryNode_ExpectWhereStatement()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();
            var lessThanFormatter = new LessThanFormatter();
            var greaterThanFormatter = new GreaterThanFormatter();
            var rangeFormatter = new RangeFormatter();

            var whereFormatter = new WhereFormatter(
                equalsFormatter,
                lessThanFormatter,
                greaterThanFormatter,
                rangeFormatter);

            var whereNode2 = new WhereNode
            {
                Statement = new WhereStatement
                {
                    Value = new EqualsOperator
                    {
                        Name = "FirstName",
                        IsNot = false,
                        Statement = "FirstName:asd",
                        Value = "asd",
                    },
                },
            };

            var whereNode = new WhereNode
            {
                Statement = new WhereStatement
                {
                    Value = new EqualsOperator
                    {
                        Name = "ColumnName",
                        IsNot = false,
                        Statement = "ColumnName:bob",
                        Value = "bob",
                    },
                },
                Conjunctive = Conjunctives.And,
                Next = whereNode2,
            };

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = whereFormatter.Format(whereNode);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Console.WriteLine(sqlDataResponse.Sql);
        }

        /// <summary>
        /// Formats the when single node expect where statement.
        /// </summary>
        [Test]
        public void Format_WhenSingleNode_ExpectWhereStatement()
        {
            // Arrange
            var equalsFormatter = new EqualsFormatter();
            var lessThanFormatter = new LessThanFormatter();
            var greaterThanFormatter = new GreaterThanFormatter();
            var rangeFormatter = new RangeFormatter();

            var whereFormatter = new WhereFormatter(
                equalsFormatter,
                lessThanFormatter,
                greaterThanFormatter,
                rangeFormatter);

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

            Console.WriteLine(sqlDataResponse.Sql);
        }
    }
}