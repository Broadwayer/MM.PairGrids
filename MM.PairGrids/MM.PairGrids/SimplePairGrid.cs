using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MM.PairGrids
{
    /// <summary>
    /// SimplePairGrid
    /// </summary>
    public class SimplePairGrid : Control
    {
        static SimplePairGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimplePairGrid), new FrameworkPropertyMetadata(typeof(SimplePairGrid)));
        }

        public static readonly DependencyProperty ItemsSourceProperty = 
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(SimplePairGrid), new PropertyMetadata(null));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ColumnNameWidthProperty =
            DependencyProperty.Register("ColumnNameWidth", typeof(GridLength), typeof(SimplePairGrid), new PropertyMetadata(new GridLength(100)));

        public GridLength ColumnNameWidth
        {
            get { return (GridLength)GetValue(ColumnNameWidthProperty); }
            set { SetValue(ColumnNameWidthProperty, value); }
        }
    }
}
