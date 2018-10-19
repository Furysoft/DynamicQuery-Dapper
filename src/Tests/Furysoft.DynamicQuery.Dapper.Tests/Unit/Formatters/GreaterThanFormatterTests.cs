﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GreaterThanFormatterTests.cs" company="Simon Paramore">
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
    /// The Greater Than Formatter Tests
    /// </summary>
    [TestFixture]
    public sealed class GreaterThanFormatterTests : TestBase
    {
        /// <summary>
        /// Formats the when inclusive expect inclusive query.
        /// </summary>
        [Test]
        public void Format_WhenInclusive_ExpectInclusiveQuery()
        {
            // Arrange
            var greaterThanFormatter = new GreaterThanFormatter();

            var node = new GreaterThanOperator
            {
                Name = "ColumnName",
                Value = 23,
                Inclusive = true,
                Statement = "ColumnName:[*,23]"
            };

            var dataDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = greaterThanFormatter.Format(node, dataDictionary);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName >= @ColumnName"));
            Assert.That(sqlDataResponse.Params["ColumnName"], Is.EqualTo(23));
        }

        /// <summary>
        /// Formats the when not inclusive expect not inclusive query.
        /// </summary>
        [Test]
        public void Format_WhenNotInclusive_ExpectNotInclusiveQuery()
        {
            // Arrange
            var greaterThanFormatter = new GreaterThanFormatter();

            var node = new GreaterThanOperator
            {
                Name = "ColumnName",
                Value = 23,
                Inclusive = false,
                Statement = "ColumnName:[*,23]"
            };

            var dataDictionary = new Dictionary<string, object>();

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlDataResponse = greaterThanFormatter.Format(node, dataDictionary);
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Assert.That(sqlDataResponse.Sql, Is.EqualTo("ColumnName > @ColumnName"));
            Assert.That(sqlDataResponse.Params["ColumnName"], Is.EqualTo(23));
        }
    }
}