// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Furysoft.DynamicQuery.Dapper.Entities;
    using Furysoft.DynamicQuery.Dapper.Exceptions;
    using Furysoft.DynamicQuery.Dapper.Interfaces.Formatters;
    using Furysoft.DynamicQuery.Entities.Operations;
    using Furysoft.DynamicQuery.Entities.QueryComponents;
    using JetBrains.Annotations;

    /// <summary>
    /// The Where Formatter.
    /// </summary>
    public sealed class WhereFormatter : IWhereFormatter
    {
        /// <summary>
        /// The equality formatter.
        /// </summary>
        [NotNull]
        private readonly IWhereOperatorFormatter<EqualsOperator> equalsFormatter;

        /// <summary>
        /// The greater than formatter.
        /// </summary>
        [NotNull]
        private readonly IWhereOperatorFormatter<GreaterThanOperator> greaterThanFormatter;

        /// <summary>
        /// The less than formatter.
        /// </summary>
        [NotNull]
        private readonly IWhereOperatorFormatter<LessThanOperator> lessThanFormatter;

        /// <summary>
        /// The less than formatter.
        /// </summary>
        [NotNull]
        private readonly IWhereOperatorFormatter<RangeOperator> rangeFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhereFormatter" /> class.
        /// </summary>
        /// <param name="equalsFormatter">The equals formatter.</param>
        /// <param name="lessThanFormatter">The less than formatter.</param>
        /// <param name="greaterThanFormatter">The greater than formatter.</param>
        /// <param name="rangeFormatter">The range formatter.</param>
        public WhereFormatter(
            [NotNull] IWhereOperatorFormatter<EqualsOperator> equalsFormatter,
            [NotNull] IWhereOperatorFormatter<LessThanOperator> lessThanFormatter,
            [NotNull] IWhereOperatorFormatter<GreaterThanOperator> greaterThanFormatter,
            [NotNull] IWhereOperatorFormatter<RangeOperator> rangeFormatter)
        {
            this.equalsFormatter = equalsFormatter;
            this.lessThanFormatter = lessThanFormatter;
            this.greaterThanFormatter = greaterThanFormatter;
            this.rangeFormatter = rangeFormatter;
        }

        /// <summary>
        /// Formats the specified where node.
        /// </summary>
        /// <param name="whereNode">The where node.</param>
        /// <returns>The <see cref="SqlDataResponse" />.</returns>
        public SqlDataResponse Format(WhereNode whereNode)
        {
            if (whereNode == null)
            {
                return null;
            }

            var sb = new StringBuilder();
            var index = 0;
            var node = whereNode;
            var paramList = new List<SqlWhereParam>();
            do
            {
                var sqlDataResponse = this.FormatLocal(node, index);
                sb.AppendFormat("{0}\r\n", sqlDataResponse.Sql);
                paramList = paramList.Concat(sqlDataResponse.Params).ToList();

                if (node.Next != null)
                {
                    sb.AppendFormat(" {0} ", node.Conjunctive);
                }

                index = sqlDataResponse.LastSuffix + 1;
                node = node.Next;
            }
            while (node != null);

            if (sb.Length == 0)
            {
                return null;
            }

            sb.Insert(0, "WHERE ");

            return new SqlDataResponse
            {
                Sql = sb.ToString(),
                Params = paramList,
                LastSuffix = index,
            };
        }

        /// <summary>
        /// Formats the local.
        /// </summary>
        /// <param name="whereNode">The where node.</param>
        /// <param name="paramSuffix">The parameter suffix.</param>
        /// <returns>The <see cref="SqlDataResponse"/>.</returns>
        private SqlDataResponse FormatLocal(WhereNode whereNode, int paramSuffix)
        {
            var statementValue = whereNode.Statement.Value;

            if (statementValue is EqualsOperator equalsOperator)
            {
                return this.equalsFormatter.Format(equalsOperator, paramSuffix);
            }

            if (statementValue is LessThanOperator lessThanOperator)
            {
                return this.lessThanFormatter.Format(lessThanOperator, paramSuffix);
            }

            if (statementValue is GreaterThanOperator greaterThanOperator)
            {
                return this.greaterThanFormatter.Format(greaterThanOperator, paramSuffix);
            }

            if (statementValue is RangeOperator rangeOperator)
            {
                return this.rangeFormatter.Format(rangeOperator, paramSuffix);
            }

            throw new SqlParseException($"Invalid statementValue type {statementValue.GetType().Name}");
        }
    }
}