using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MM.PairGrids
{
    public class BasicContentTemplateSelector : DataTemplateSelector
    {
        public required DataTemplate StringContentTemplate { get; set; }
        public required DataTemplate BooleanContentTemplate { get; set; }
        public required DataTemplate IntegerContentTemplate { get; set; }
        public required DataTemplate FloatContentTemplate { get; set; }
        public required DataTemplate DoubleContentTemplate { get; set; }


        public required DataTemplate OptionsContentTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TypedItem<string>)
                return StringContentTemplate;
            else if (item is TypedItem<bool>)
                return BooleanContentTemplate;
            else if (item is TypedItem<int>)
                return IntegerContentTemplate;
            else if (item is TypedItem<float>)
                return FloatContentTemplate;
            else if (item is TypedItem<double>)
                return DoubleContentTemplate;
            else if (item is OptionalTypedItem<string>)
                return OptionsContentTemplate;
            else
                return base.SelectTemplate(item, container);
        }
    }

    public class ItemTemplateSelector : DataTemplateSelector
    {
        public required DataTemplate ValueHolderItemTemplate { get; set; }
        public required DataTemplate GroupItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is GroupItem)
                return GroupItemTemplate;
            else if (item is IValueHolder)
                return ValueHolderItemTemplate;
            else
                return base.SelectTemplate(item, container);
        }
    }

    public class BoolToExpandedCollapsedSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "-" : "+";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleBinding : DependencyObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(double),
                typeof(DoubleBinding),
                new PropertyMetadata(0.0, OnValueChanged));

        public static double GetValue(DependencyObject obj) => (double)obj.GetValue(ValueProperty);

        public static void SetValue(DependencyObject obj, double value) => obj.SetValue(ValueProperty, value);

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.PreviewMouseDown -= OnPreviewMouseDown;
                textBox.PreviewLostKeyboardFocus -= OnLostFocus;
                textBox.TextChanged -= OnTextChanged;
                if (e.NewValue is double)
                    textBox.Text = ((double)e.NewValue).ToString(CultureInfo.CurrentCulture);
                textBox.TextChanged += OnTextChanged;
                textBox.PreviewLostKeyboardFocus += OnLostFocus;
                textBox.PreviewMouseDown += OnPreviewMouseDown;
            }
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null &&
                sender is TextBox textBox &&
                !double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double result))
            {
                MessageBox.Show("Invalid value");
                e.Handled = true;
            }
        }

        private static void OnPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender != null &&
                sender is TextBox textBox &&
                !double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double result))
            {
                MessageBox.Show("Invalid value");
                e.Handled = true;
                textBox.Focus();
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ValidateTextBox(textBox);
            }
        }

        private static void ValidateTextBox(TextBox textBox)
        {
            if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double result))
            {
                SetValue(textBox, result);
                textBox.BorderBrush = System.Windows.Media.Brushes.Black;
            }
            else
            {
                textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox.Focus();
            }
        }
    }

    public class FloatBinding : DependencyObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(float),
                typeof(FloatBinding),
                new PropertyMetadata(0.0f, OnValueChanged));

        public static float GetValue(DependencyObject obj) => (float)obj.GetValue(ValueProperty);

        public static void SetValue(DependencyObject obj, float value) => obj.SetValue(ValueProperty, value);
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.PreviewMouseDown -= OnPreviewMouseDown;
                textBox.PreviewLostKeyboardFocus -= OnLostFocus;
                textBox.TextChanged -= OnTextChanged;
                if (e.NewValue is float)
                    textBox.Text = ((float)e.NewValue).ToString(CultureInfo.CurrentCulture);
                textBox.TextChanged += OnTextChanged;
                textBox.PreviewLostKeyboardFocus += OnLostFocus;
                textBox.PreviewMouseDown += OnPreviewMouseDown;
            }
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null &&
                sender is TextBox textBox &&
                !float.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out float result))
            {
                MessageBox.Show("Invalid value");
                e.Handled = true;
            }
        }

        private static void OnPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender != null &&
                sender is TextBox textBox &&
                !float.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out float result))
            {
                MessageBox.Show("Invalid value");
                e.Handled = true;
                textBox.Focus();
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ValidateTextBox(textBox);
            }
        }

        private static void ValidateTextBox(TextBox textBox)
        {
            if (float.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out float result))
            {
                SetValue(textBox, result);
                textBox.BorderBrush = System.Windows.Media.Brushes.Black;
                textBox.BorderThickness = new Thickness(0);
            }
            else
            {
                textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox.BorderThickness = new Thickness(1);
                textBox.Focus();
            }
        }
    }

    public class IntegerBinding : DependencyObject
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
                "Value",
                typeof(int),
                typeof(IntegerBinding),
                new PropertyMetadata(0, OnValueChanged));

        public static int GetValue(DependencyObject obj) => (int)obj.GetValue(ValueProperty);

        public static void SetValue(DependencyObject obj, int value) => obj.SetValue(ValueProperty, value);
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.PreviewMouseDown -= OnPreviewMouseDown;
                textBox.PreviewLostKeyboardFocus -= OnLostFocus;
                textBox.TextChanged -= OnTextChanged;
                if (e.NewValue is int)
                    textBox.Text = ((int)e.NewValue).ToString(CultureInfo.CurrentCulture);
                textBox.TextChanged += OnTextChanged;
                textBox.PreviewLostKeyboardFocus += OnLostFocus;
                textBox.PreviewMouseDown += OnPreviewMouseDown;
            }
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null &&
                sender is TextBox textBox &&
                !int.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out int result))
            {
                MessageBox.Show("Invalid value");
                e.Handled = true;
            }
        }

        private static void OnPreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender != null &&
                sender is TextBox textBox &&
                !float.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out float result))
            {
                MessageBox.Show("Invalid value");
                e.Handled = true;
                textBox.Focus();
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                ValidateTextBox(textBox);
            }
        }

        private static void ValidateTextBox(TextBox textBox)
        {
            if (int.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out int result))
            {
                SetValue(textBox, result);
                textBox.BorderBrush = System.Windows.Media.Brushes.Black;
                textBox.BorderThickness = new Thickness(0);
            }
            else
            {
                textBox.BorderBrush = System.Windows.Media.Brushes.Red;
                textBox.BorderThickness = new Thickness(1);
                textBox.Focus();
            }
        }
    }
}
