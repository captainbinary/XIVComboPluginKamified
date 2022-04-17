using System;
using System.Runtime.CompilerServices;

namespace XIVComboKamifiedPlugin.Attributes
{
    /// <summary>
    /// An attribute that allows for sorting an enum by declaration order.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class OrderedEnumAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedEnumAttribute"/> class.
        /// </summary>
        /// <param name="order">Caller line number, should not be used.</param>
        internal OrderedEnumAttribute([CallerLineNumber] int order = 0)
        {
            this.Order = order;
        }

        /// <summary>
        /// Gets the declaration order.
        /// </summary>
        public int Order { get; }
    }
}
