using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Common.Classes
{
    public class BaseViewableCollection<T> : BaseObservableCollection<T> where T : INotifyPropertyChanged
    {
        public BaseViewableCollection() : base() { }

        public BaseViewableCollection(IEnumerable<T> items) : base(items) { }

        public BaseViewableCollection(List<T> items) : base(items) { }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender);
            base.OnCollectionChanged(arg);
        }

        protected override void ClearItems()
        {
            foreach (T item in Items) { if (item != null) { item.PropertyChanged -= ItemPropertyChanged; } }
            base.ClearItems();
        }

        protected override void InsertItem(int index, T item)
        {
            if (item != null) { item.PropertyChanged += ItemPropertyChanged; }
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (Items[index] != null) { Items[index].PropertyChanged -= ItemPropertyChanged; }
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            if (item != null) { item.PropertyChanged += ItemPropertyChanged; }
            base.SetItem(index, item);
        }

    }
}
