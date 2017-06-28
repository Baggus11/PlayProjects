using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common
{
    public abstract class ViewModelBase : /*BindableBase,*/ INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private readonly Dictionary<string, List<string>> _dependencies = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, object> _propertyValueStorage;

        public ViewModelBase()
        {
            DetectPropertiesDependencies();
            this._propertyValueStorage = new Dictionary<string, object>();

        }

        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            CheckPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Conditional("DEBUG")]
        private void CheckPropertyName(string propertyName)
        {
            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)[propertyName];
            if (propertyDescriptor == null)
            {
                throw new InvalidOperationException(string.Format(null, "The property with the propertyName '{0}' doesn't exist.", propertyName));
            }
        }

        private string getPropertyName(LambdaExpression lambdaExpression)
        {
            MemberExpression memberExpression;

            if (lambdaExpression.Body is UnaryExpression)
            {
                var unaryExpression = lambdaExpression.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambdaExpression.Body as MemberExpression;
            }

            return memberExpression.Member.Name;
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            bool hasChanged = EqualityComparer<T>.Default.Equals(storage, value);

            storage = value;
            RaisePropertyChanged(propertyName);

            return hasChanged;
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");

            MemberExpression body = selectorExpression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("The body must be a member expression");

            RaisePropertyChanged(body.Member.Name);
        }

        protected void RaisePropertiesDependentOn(params Expression<Func<object>>[] expressions)//rename as work name
        {
            expressions.Select(expression => GetPropertyName(expression)).ToList().ForEach(propertyName =>
            {
                RaisePropertyChanged(propertyName);
                RaiseDependentProperties(propertyName, new List<string>() { propertyName });
            });

        }

        private void RaiseDependentProperties(string propertyName, List<string> calledProperties = null)
        {
            if (!_dependencies.Any() || !_dependencies.ContainsKey(propertyName))
                return;

            if (calledProperties == null)
                calledProperties = new List<string>();

            List<string> dependentProperties = _dependencies[propertyName];

            foreach (var dependentProperty in dependentProperties)
            {
                if (!calledProperties.Contains(dependentProperty))
                {
                    RaisePropertyChanged(dependentProperty);
                    RaiseDependentProperties(dependentProperty, calledProperties);
                }
            }
        }

        private string GetPropertyName(Expression<Func<object>> expression)
        {
            var expr = (MemberExpression)expression.Body;

            return expr.Member.Name;

        }

        private void DetectPropertiesDependencies()
        {
            var propertyInfoWithDependencies = GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(DependentPropertiesAttribute))).ToArray();

            foreach (PropertyInfo propertyInfo in propertyInfoWithDependencies)
            {
                var ca = propertyInfo.GetCustomAttributes(false).OfType<DependentPropertiesAttribute>().Single();
                if (ca.Properties != null)
                {
                    foreach (string prop in ca.Properties)
                    {
                        if (!_dependencies.ContainsKey(prop))
                        {
                            _dependencies.Add(prop, new List<string>());
                        }

                        _dependencies[prop].Add(propertyInfo.Name);
                    }
                }
            }
        }

        protected void SetValue<T>(Expression<Func<T>> property, T value)
        {
            var lambdaExpression = property as LambdaExpression;

            if (lambdaExpression == null)
            {
                throw new ArgumentException("Invalid lambda expression", "Lambda expression return value can't be null");
            }

            var propertyName = getPropertyName(lambdaExpression);
            var storedValue = getValue<T>(propertyName);

            if (Equals(storedValue, value)) return;

            _propertyValueStorage[propertyName] = value;
            RaisePropertyChanged(propertyName);
        }

        protected T GetValue<T>(Expression<Func<T>> property)
        {
            var lambdaExpression = property as LambdaExpression;

            if (lambdaExpression == null)
            {
                throw new ArgumentException("Invalid lambda expression", "Lambda expression return value can't be null");
            }

            var propertyName = this.getPropertyName(lambdaExpression);
            return getValue<T>(propertyName);
        }

        private T getValue<T>(string propertyName)
        {
            object value;
            if (_propertyValueStorage.TryGetValue(propertyName, out value))
            {
                return (T)value;
            }
            return default(T);

        }

    }
    public class DependentPropertiesAttribute : Attribute
    {
        private readonly string[] properties;

        public DependentPropertiesAttribute(params string[] dp)
        {
            properties = dp;
        }

        public string[] Properties
        {
            get
            {
                return properties;
            }
        }
    }





}