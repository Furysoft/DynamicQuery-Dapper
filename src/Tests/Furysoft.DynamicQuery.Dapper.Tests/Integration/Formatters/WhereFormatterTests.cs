// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereFormatterTests.cs" company="Simon Paramore">
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
    using Logic.Formatters;
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
            var equalsFormatter = new EqualsFormatter();
            var lessThanFormatter = new LessThanFormatter();
            var greaterThanFormatter = new GreaterThanFormatter();
            var rangeFormatter = new RangeFormatter();

            var whereFormatter = new WhereFormatter(
                equalsFormatter,
                lessThanFormatter,
                greaterThanFormatter,
                rangeFormatter);

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

            Console.WriteLine(sqlDataResponse.Sql);
        }

        /// <summary>
        /// Formats the when binary node expect where statement.
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

            var whereNode1 = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Statement = "ColumnName:bob",
                Value = "bob"
            };

            var whereNode2 = new EqualsOperator
            {
                Name = "FirstName",
                IsNot = false,
                Statement = "FirstName:asd",
                Value = "asd"
            };

            var whereNode = new BinaryNode
            {
                Name = "asd",
                Statement = "ColumnName:bob and FirstName:asd",
                Conjunctive = Conjunctives.And,
                LeftNode = whereNode1,
                RightNode = whereNode2
            };

            var dataDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = whereFormatter.Format(whereNode, dataDictionary);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Console.WriteLine(sqlDataResponse.Sql);
        }

        /// <summary>
        /// Formats this instance.
        /// </summary>
        [Test]
        public void Format()
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

            var whereNode1 = new EqualsOperator
            {
                Name = "ColumnName",
                IsNot = false,
                Statement = "ColumnName:bob",
                Value = "bob"
            };

            var whereNode2 = new EqualsOperator
            {
                Name = "FirstName",
                IsNot = false,
                Statement = "FirstName:asd",
                Value = "asd"
            };

            var whereNode = new BinaryNode
            {
                Name = "asd",
                Statement = "ColumnName:bob and FirstName:asd",
                Conjunctive = Conjunctives.And,
                LeftNode = whereNode1,
                RightNode = whereNode2
            };

            var dataDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = whereFormatter.Format(whereNode, dataDictionary);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Console.WriteLine(sqlDataResponse.Sql);
        }
    }
}