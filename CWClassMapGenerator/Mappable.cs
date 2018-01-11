using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CWClassMapGenerator
{
    public abstract class Mappable : IMappable, INotifyPropertyChanged
    {
        private Dictionary<Type, PropertyInfo[]> friendMap = new Dictionary<Type, PropertyInfo[]>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected Type DerivedType { get; private set; }

        protected PropertyInfo[] DerivedProperties { get; private set; }

        protected Mappable(Type derivedType) => DerivedType = derivedType;

        protected Mappable(Type derivedType, PropertyInfo[] derivedProperties)
        {
            DerivedType = derivedType;
            DerivedProperties = derivedProperties;
        }

        public object Clone() => MemberwiseClone();

        public void Friend<TFriend>() => friendMap.Add(typeof(TFriend), default(TFriend).GetType().GetProperties());

        public void Bind<TFriend>(TFriend friend)
        {
            var type = typeof(TFriend);

            if (friendMap.Keys.ToList().Contains(type))
            {
                return;
            }

            friendMap.Add(type, type.GetProperties());

            //If any properties changed, fire 'OnPropertyChanged()' for those.
        }

        //public void OnPropertyChangedEvent()
        //{
        //    //Update all registered observers with the properties that have been changed (and their values!)
        //}
    }
}
