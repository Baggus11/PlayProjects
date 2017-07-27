using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Markup;

namespace Common
{
    public static class EnumExtensions
    {
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

        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static T Previous<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argumnent {0} is not an Enum", typeof(T).FullName));
            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) - 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        /// <summary>
        /// Intended for enums to create conditions that are comparable to instances with given enum as a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType"></param>
        /// <returns></returns>
        /// <example>
        /// var condition = "CurrentUser.CurrentTeamType == \"Admin\"";
        /// </example>
        public static string ToExpressionEnumCondition<T>(this T enumType)
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException($"Type {typeof(T).Name} is not a valid Enum.");

            StringBuilder comparableEnumCondition = new StringBuilder();

            comparableEnumCondition.Append("\"");
            comparableEnumCondition.Append(enumType.ToString());
            comparableEnumCondition.Append("\"");

            return comparableEnumCondition.ToString();
        }

        public static T GetRandomEnumValue<T>()
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(new Random().Next(values.Length));
        }

        public static Enum GetRandomEnumValue(this Type t)
        {
            return Enum.GetValues(t)
                .OfType<Enum>()
                .OrderBy(e => Guid.NewGuid())
                .FirstOrDefault();
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

        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType)
        {
            EnumType = enumType;
        }

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
