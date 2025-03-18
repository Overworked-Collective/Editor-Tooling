#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static PlasticPipe.Client.InvokeMethodRetry;
using Tooling.Reflection;

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
            string[] selectedPath = splitPath.Where(s => !s.Equals(property.name)).ToArray();
            
/*            if (selectedPath.Where(s => s.Contains("[")).ToArray().Length > 0 )
            {
                return "";
            }*/
            
            return selectedPath.Fuse('.');
        }

/*        public static T GetAttribute<T>(this SerializedProperty property) where T : Attribute
        {
            FieldInfo fieldInfo = property.GetFieldInfo();
            Debug.Log(fieldInfo.Name);

            object[] attributes = fieldInfo.GetCustomAttributes(false).Where(a => a.GetType() == typeof(T)).ToArray();
            if (attributes.Length > 0)
            {
                return attributes[0] as T;
            }

            return null;
        }*/

/*        public static FieldInfo GetFieldInfo(this SerializedProperty property)
        {
            return ReflectionUtility.GetField(property.serializedObject.targetObject, property);
        }*/

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
