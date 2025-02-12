#if UNITY_EDITOR

using System.Linq;
using UnityEditor;

namespace Tooling.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static bool IsArrayElement(this SerializedProperty property)
        {
            return property.propertyPath.Contains("Array");
        }

        public static int GetElementIndex(this SerializedProperty property)
        {
            if (!IsArrayElement(property)) { return 0; }

            string[] splitPath = property.propertyPath.Split('.');
            string[] dataElements = splitPath.Where(s => s.Contains("data")).ToArray();

            string str = dataElements.Last()[5].ToString();

            int index;
            int.TryParse(str, out index);
            return index;
        }
    }
}

#endif
