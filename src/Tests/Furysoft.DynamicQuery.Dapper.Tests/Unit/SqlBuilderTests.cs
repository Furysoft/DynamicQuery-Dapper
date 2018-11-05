// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlBuilderTests.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Tests.Unit
{
    using System;
    using System.Diagnostics;
    using Attributes;
    using NUnit.Framework;

    /// <summary>
    /// The SQL Builder Tests
    /// </summary>
    [TestFixture]
    public sealed class SqlBuilderTests : TestBase
    {
        /// <summary>
        /// Builds the when query in expect SQL built.
        /// </summary>
        [Test]
        public void Build_WhenQueryIn_ExpectSqlBuilt()
        {
            // Arrange
            var dynamicQueryParser = new DynamicQueryParser();
            var query1 = dynamicQueryParser.Parse<TestEntity>("where::column_id:1 and Name:bob and Age:[18,32}orderby::column_id asc page::1,10");

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlEntity = query1.CreateSqlQuery()
                .Select("ColumnId, Name, Age")
                .From("users")
                .Build();
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Console.WriteLine(sqlEntity.Sql);
        }

        /// <summary>
        /// Builds the when query in with count cte expect SQL built.
        /// </summary>
        [Test]
        public void Build_WhenQueryInWithCountCte_ExpectSqlBuilt()
        {
            // Arrange
            var dynamicQueryParser = new DynamicQueryParser();
            var query1 = dynamicQueryParser.Parse<TestEntity>("where::column_id:1 and Name:bob and Age:[18,32}orderby::column_id asc page::1,10");

            // Act
            var stopwatch = Stopwatch.StartNew();
            var sqlEntity = query1.CreateSqlQuery()
                .WithCountCte()
                .Select("ColumnId, Name, Age")
                .From("users")
                .Build();
            stopwatch.Stop();

            // Assert
            this.WriteTimeElapsed(stopwatch);

            Console.WriteLine(sqlEntity.Sql);
        }

        /// <summary>The Test Entity</summary>
        private sealed class TestEntity
        {
            /// <summary>Gets or sets the age.</summary>
            public int Age { get; set; }

            /// <summary>Gets or sets the column identifier.</summary>
            [Name("column_id")]
            public string ColumnId { get; set; }

            /// <summary>Gets or sets the name.</summary>
            public string Name { get; set; }
        }
    }
}