using System;
using System.Windows;

namespace WPFGUIProjectFMI
{
    /// <summary>
    /// The base attached property
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    /// <typeparam name="Property"></typeparam>
    public abstract class BaseAttachedProperty<Parent, Property>
        where Parent : new()
    {
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };
        public static Parent Instance { get; private set; } = new Parent();
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), 
            typeof(BaseAttachedProperty<Parent, Property>), 
            new UIPropertyMetadata(
                default(Property),
                new PropertyChangedCallback(OnValuePropertyChanged),
                new CoerceValueCallback(OnValuePropertyUpdated)
                ));

        /// <summary>
        /// Property changed event fired
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueChanged(d, e);
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueChanged(d, e);
        }

        /// <summary>
        /// Property updated event fired
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueUpdated(d, value);
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueUpdated(d, value);
            return value;
        }
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }

    }
}
