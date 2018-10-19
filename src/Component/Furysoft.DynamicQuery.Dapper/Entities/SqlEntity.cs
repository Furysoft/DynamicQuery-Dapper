// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlEntity.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Entities
{
    /// <summary>
    /// The SQL Entity
    /// </summary>
    public sealed class SqlEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlEntity" /> class.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="data">The data.</param>
        public SqlEntity(string sql, object data)
        {
            this.Sql = sql;
            this.Data = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// Gets the SQL.
        /// </summary>
        public string Sql { get; }
    }
}