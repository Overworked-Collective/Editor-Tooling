using System;
using System.Linq;

namespace Tooling.Extensions
{
    public static class ArrayExtensions
    {
        public static bool All<T>(this T[] array, Func<T, bool> predecate)
        {
            return array.Where(predecate).ToList().Count == array.Length;
        }

        public static string ToCSV<T>(this T[] array)
        {
            string csvString = "";
            foreach (var item in array)
            {
                csvString += item.ToString();
                if (!array.Last().Equals(item))
                {
                    csvString += ", ";
                }
            }
            return csvString;
        }
    }
}
