#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static PlasticPipe.Client.InvokeMethodRetry;

namespace Tooling.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static bool IsArrayElement(this SerializedProperty property)
        {
            return property.propertyPath.Contains("Array");
        }

        /*        public static Type GetContainerType(this SerializedProperty property)
                {
                    string[] splitPath = property.propertyPath.Split('.');
                    List<string> selectedPathComponents = new List<string>();
                    for (int i = 0; i < splitPath.Length; i++)
                    {
                        if (splitPath[i].Contains("data["))
                        {
                            selectedPathComponents.Remove(selectedPathComponents.Last());
                        } 
                        else
                        {
                            selectedPathComponents.Add(splitPath[i]);
                        }
                    }

                    if (selectedPathComponents.Count <= 1) { return null; }

                    Debug.Log(selectedPathComponents.ToArray().ToCSV());
                    Debug.Log(selectedPathComponents[^2]);

                    //return null;
                    return Type.GetType("TestClass", false, true);
                }*/

        public static string GetContainerPath(this SerializedProperty property)
        {
            string[] splitPath = property.propertyPath.Split('.');
            string[] selectedPath = splitPath.Where(s => !s.Contains(property.name)).ToArray();
            return selectedPath.Fuse('.');
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
