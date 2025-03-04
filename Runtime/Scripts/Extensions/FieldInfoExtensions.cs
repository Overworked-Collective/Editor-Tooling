using System;
using System.Linq;
using System.Reflection;

namespace Tooling.Extensions
{
    public static class FieldInfoExtensions
    {
        public static T GetAttribute<T>(this FieldInfo fieldInfo) where T : Attribute
        {
            object[] attributes = fieldInfo.GetCustomAttributes(false).Where(a => a.GetType() == typeof(T)).ToArray();
            if (attributes.Length > 0)
            {
                return attributes[0] as T;
            }
            return null;
        }
    }
}
