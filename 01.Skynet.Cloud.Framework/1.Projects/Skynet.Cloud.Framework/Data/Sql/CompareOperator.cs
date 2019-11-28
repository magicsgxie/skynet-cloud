using System;

namespace UWay.Skynet.Cloud.Data
{
    /// <summary>
    /// Specifies how tow operands are to be compared
    /// </summary>
    public enum CompareOperator
    {
        /// <summary>Equal</summary>
        Equal,
        /// <summary>Different</summary>
        NotEqual,
        /// <summary>Left operand is greater</summary>
        Greater,
        /// <summary>Left operand is less</summary>
        Less,
        /// <summary>Left operand is less or equal</summary>
        LessOrEqual,
        /// <summary>Left operand is greater or equal</summary>
        GreaterOrEqual,
        /// <summary>Make a bitwise AND and check the result for being not null (ex: (a &amp; b) > 0) ) </summary>
        BitwiseAnd,
        /// <summary>Substring. Use '%' signs in the value to match anything</summary>
        Like,

        /// <summary>
        /// ¿Õ
        /// </summary>
        IsNull,

        /// <summary>
        /// ·Ç¿Õ
        /// </summary>
        IsNotNull 
    }
}
