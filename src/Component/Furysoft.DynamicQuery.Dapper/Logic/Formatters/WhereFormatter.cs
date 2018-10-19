// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereFormatter.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Logic.Formatters
{
    using System.Collections.Generic;
    using System.Text;
    using DynamicQuery.Entities.Nodes;
    using DynamicQuery.Entities.Operations;
    using Entities;
    using Interfaces.Formatters;
    using JetBrains.Annotations;

    /// <summary>
    /// The Where Formatter
    /// </summary>
    public sealed class WhereFormatter : IWhereFormatter
    {
        /// <summary>
        /// The equality formatter
        /// </summary>
        [NotNull]
        private readonly IWhereOperatorFormatter<EqualsOperator> equalsFormatter;

        /// <summary>
        /// The greater than formatter
        /// </summary>
        [NotNull]
        private readonly IWhereOperatorFormatter<GreaterThanOperator> greaterThanFormatter;

        /// <summary>
        /// The less than formatter
        /// </summary>
        [NotNull]
        private readonly IWhereOperatorFormatter<LessThanOperator> lessThanFormatter;

        /// <summary>
        /// The less than formatter
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
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <returns>The <see cref="SqlDataResponse"/></returns>
        public SqlDataResponse Format(Node whereNode, IDictionary<string, object> dataDictionary)
        {
            var sb = new StringBuilder();

            this.FormatLocal(whereNode, dataDictionary, sb);

            if (sb.Length == 0)
            {
                return null;
            }

            sb.Insert(0, "WHERE ");

            return new SqlDataResponse
            {
                Sql = sb.ToString(),
                Params = dataDictionary
            };
        }

        /// <summary>
        /// Formats the local.
        /// </summary>
        /// <param name="whereNode">The where node.</param>
        /// <param name="dataDictionary">The data dictionary.</param>
        /// <param name="stringBuilder">The string builder.</param>
        private void FormatLocal(
            Node whereNode,
            IDictionary<string, object> dataDictionary,
            StringBuilder stringBuilder)
        {
            if (whereNode is BinaryNode binaryNode)
            {
                this.FormatLocal(binaryNode.LeftNode, dataDictionary, stringBuilder);
                stringBuilder.AppendFormat(" {0} ", binaryNode.Conjunctive);
                this.FormatLocal(binaryNode.RightNode, dataDictionary, stringBuilder);
            }

            if (whereNode is EqualsOperator equalsOperator)
            {
                var sqlDataResponse = this.equalsFormatter.Format(equalsOperator, dataDictionary);
                stringBuilder.AppendFormat("{0}\r\n", sqlDataResponse.Sql);
            }

            if (whereNode is LessThanOperator lessThanOperator)
            {
                var sqlDataResponse = this.lessThanFormatter.Format(lessThanOperator, dataDictionary);
                stringBuilder.AppendFormat("{0}\r\n", sqlDataResponse.Sql);
            }

            if (whereNode is GreaterThanOperator greaterThanOperator)
            {
                var sqlDataResponse = this.greaterThanFormatter.Format(greaterThanOperator, dataDictionary);
                stringBuilder.AppendFormat("{0}\r\n", sqlDataResponse.Sql);
            }

            if (whereNode is RangeOperator rangeOperator)
            {
                var sqlDataResponse = this.rangeFormatter.Format(rangeOperator, dataDictionary);
                stringBuilder.AppendFormat("{0}\r\n", sqlDataResponse.Sql);
            }
        }
    }
}