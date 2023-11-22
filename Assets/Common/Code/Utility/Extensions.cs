using System;
using System.Linq;

namespace TatmanGames.Common.Utility
{
    /// <summary>
    /// see https://stackoverflow.com/questions/2656189/how-do-i-read-an-attribute-on-a-class-at-runtime
    /// use
    /// string name = typeof(MyClass).GetAttributeValue((CustomAttribute c) => c.WhateverProperty);
    /// </summary>
    public static class TypeExceptions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type, 
            Func<TAttribute, TValue> valueSelector) 
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }
    }
}