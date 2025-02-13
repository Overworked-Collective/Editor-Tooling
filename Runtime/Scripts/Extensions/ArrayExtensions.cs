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
            return array.Fuse(',');
        }

        public static string Fuse<T>(this T[] array, char seperator)
        {
            string returnString = "";
            foreach (var item in array)
            {
                returnString += item.ToString();
                if (!array.Last().Equals(item))
                {
                    returnString += seperator;
                }
            }
            return returnString;
        }
    }
}
