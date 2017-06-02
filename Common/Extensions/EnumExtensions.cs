using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Markup;
namespace Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the Description Custom Attribute of an Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Description Attribute value as String</returns>
        /// <remarks></remarks>
        public static string GetDescription(this Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return Convert.ToString(((attributes.Length > 0) ? attributes[0].Description : value.ToString()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString()));
                return "";
            }
        }
        /// <summary>
        /// Gets the next enum from the set defined
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
        /// <summary>
        /// Gets the previous enum from the set defined
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Previous<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) - 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        /// <summary>
        /// Intended for enums to create conditions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType"></param>
        /// <returns></returns>
        /// <example>
        /// var condition = "CurrentUser.CurrentTeamType == \"Admin\"";
        /// </example>
        public static string ToExpressionCondition<T>(this T enumType)
        {
            StringBuilder result = new StringBuilder();
            result.Append("\"");
            result.Append(enumType.ToString());
            result.Append("\"");
            return result.ToString();
        }
    }
    /// <summary>
    /// Enum Binding Source
    /// 
    /// Allows the Binding of any enum you specify via namespace
    /// Example XAML: ItemsSource="{Binding Source={local:EnumBindingSource EnumType=regexns:RegexOptions}}"
    /// Paste This XAML to your WPF: 	
    ///		xmlns:local="clr-namespace:$YourLocalNamespaceHere$"
    /// </summary>
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return this._enumType; }
            set
            {
                if (value != this._enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }
                    this._enumType = value;
                }
            }
        }
        /// <summary>
        /// Enum Binding Source Extension
        /// </summary>
        public EnumBindingSourceExtension() { }
        /// <summary>
        /// Enum Binding Source Extension
        /// </summary>
        /// <param name="enumType"></param>
        public EnumBindingSourceExtension(Type enumType)
        {
            EnumType = enumType;
        }
        /// <summary>
        /// Provide Value
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == this._enumType)
                throw new InvalidOperationException("The EnumType must be specified.");
            Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
            //Get all the enum values to an array:
            Array enumValues = Enum.GetValues(actualEnumType);
            //If enum type matches, return:
            if (actualEnumType == this._enumType)
                return enumValues;
            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}
