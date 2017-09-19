using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Common
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAssignableTypes<T>()
        {
            try
            {
                var assignableTypes = (from t in Assembly.Load(typeof(T).Namespace).GetExportedTypes()
                                       where !t.IsInterface && !t.IsAbstract
                                       where typeof(T).IsAssignableFrom(t)
                                       select t).ToArray();
                return assignableTypes;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static Dictionary<Type, UInterface> GetRepositories<TDerived, UInterface>()
            where UInterface : class, TDerived
        {
            return Assembly.GetAssembly(typeof(TDerived))
               .GetTypes()
               .Where(x => x.BaseType != null &&
                           x.BaseType.GetGenericArguments().FirstOrDefault() != null)
               .ToDictionary(x => x.BaseType.GetGenericArguments().FirstOrDefault(),
                            x => Activator.CreateInstance(x) as UInterface);

        }

        /// <summary>
        /// Invoke Method from an Instance (non-null)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="instanceMethodName"></param>
        public static void InvokeMethod<T>(this T instance, string instanceMethodName, object[] parameters)
        {
            try
            {
                Type type = typeof(T);
                MethodInfo toInvoke = type.GetMethod(instanceMethodName);
                Debug.WriteLine(toInvoke.Invoke(instance, parameters));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Invoke Static Method from a given Type
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="staticMethodName"></param>
        /// <param name="parameters"></param>
        public static void InvokeMethod(this Type classType, string staticMethodName, object[] parameters)
        {
            try
            {
                MethodInfo toInvoke = classType.GetMethod(staticMethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (toInvoke != null)
                    toInvoke.Invoke(null, parameters);
                else
                    Debug.WriteLine($"Method '{staticMethodName}' not found in class '{classType.FullName}'!");//Make sure you are calling a static method!
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Invoke Generic Method from a given class Type on a new type 'genericMethodType'
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="genericMethodName"></param>
        /// <param name="genericMethodType"></param>
        public static void InvokeMethod(this Type classType, string genericMethodName, object[] parameters, Type genericMethodType)
        {
            try
            {
                object instance = Activator.CreateInstance(classType);
                MethodInfo openMethod = classType.GetMethod(genericMethodName);
                MethodInfo toInvoke = openMethod.MakeGenericMethod(genericMethodType);
                toInvoke.Invoke(instance, parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

    }
}
