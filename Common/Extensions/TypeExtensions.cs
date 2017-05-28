using System;
using System.Diagnostics;
using System.Reflection;
namespace Common.Extensions
{
    public static class TypeExtensions
    {
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
