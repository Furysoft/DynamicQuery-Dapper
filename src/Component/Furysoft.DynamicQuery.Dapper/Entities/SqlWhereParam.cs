// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlWhereParam.cs" company="Simon Paramore">
// © 2017, Simon Paramore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Furysoft.DynamicQuery.Dapper.Entities
{
    /// <summary>
    /// The SQL Where Clause.
    /// </summary>
    public sealed class SqlWhereParam
    {
        /// <summary>Gets or sets the value.</summary>
        public object Value { get; set; }

        /// <summary>Gets or sets the name of the variable.</summary>
        public string VarName { get; set; }
    }
}