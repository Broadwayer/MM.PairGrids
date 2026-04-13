using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MM.PairGrids
{
    public abstract class BasicItem : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Indentation { get; set; } = "";
        public string Title { get; set; }
        public bool IsEnabled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IValueHolder
    {
        object GetValueAsObject();
        void SetValueFromObject(object value);
    }

    public class TypedItem<T> : BasicItem, IValueHolder
    {
        T _value;

        public T Value
        {
            get => _value;
            set { _value = value; OnPropertyChanged(); }
        }

        object IValueHolder.GetValueAsObject()
        {
            return Value;
        }

        void IValueHolder.SetValueFromObject(object value)
        {
            if (value is T typedValue)
            {
                Value = typedValue;
            }
        }
    }

    public abstract class OptionalTypedItem<T> : BasicItem, IValueHolder
    {
        T _value;
        public T Value
        {
            get => _value;
            set { _value = value; OnPropertyChanged(); }
        }
        object IValueHolder.GetValueAsObject()
        {
            return Value;
        }
        void IValueHolder.SetValueFromObject(object value)
        {
            if (value is T typedValue && Options.Contains<T>(typedValue))
            {
                Value = typedValue;
            }
        }

        public T[] Options { get; protected set; }
    }

    public class GroupItem : BasicItem
    {
        public ObservableCollection<BasicItem> SubItems { get; private set; } = new ObservableCollection<BasicItem>();

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
                OnPropertyChanged(nameof(ChildrenVisibility));
            }
        }

        public Visibility ChildrenVisibility => IsExpanded ? Visibility.Visible : Visibility.Collapsed;
    }
}
