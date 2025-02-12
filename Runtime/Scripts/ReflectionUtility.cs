using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Tooling.Reflection
{
    public static class ReflectionUtility
    {
        public static IEnumerable<MethodInfo> GetAllMethods(object target, Func<MethodInfo, bool> predicate)
        {
            if (target == null)
            {
                Debug.LogError("Target was null. Could not use null to get methods");
                yield break;
            }

            List<Type> types = GetAllTypes(target);

            for (int i = 0; i < types.Count; i++)
            {
                IEnumerable<MethodInfo> methodInfos = types[i].GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly).Where(predicate);

                foreach (var methodInfo in methodInfos)
                {
                    yield return methodInfo;
                }
            }
        }

        private static List<Type> GetAllTypes(object target)
        {
            List<Type> types = new List<Type>();
            types.Add(target.GetType());

            while(types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            return types;
        }
    }
}
