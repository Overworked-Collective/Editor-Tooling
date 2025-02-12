using UnityEngine;

namespace Tooling.Extensions
{
    public static class StringExtensions
    {
        public static string AddSpacesBeforeUpper(this string str)
        {
            string retStr = "";
            int index = 0;
            foreach (char c in str)
            {
                if (char.IsUpper(c) && index != 0)
                {
                    retStr += " ";
                }
                retStr += c;
                index++;
            }
            return retStr;
        }

        public static Vector2 GetTextScale(this string text)
        {
            GUIStyle style = new GUIStyle();
            Vector2 stringSize = style.CalcSize(new GUIContent(text));
            return stringSize;
        }

        public static string ToLowerWithoutSpaces (this string text)
        {
            string str = ""; 
            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                if (character != ' ')
                {
                    str += character;
                }
            }
            return str.ToLower();
        }
    }
}
