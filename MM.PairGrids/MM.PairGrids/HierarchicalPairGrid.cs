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
    /// A control that displays a hierarchical collection of items in a grid format, where each item can have child items.
    /// </summary>
    public class HierarchicalPairGrid : Control
    {
        static HierarchicalPairGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HierarchicalPairGrid), new FrameworkPropertyMetadata(typeof(HierarchicalPairGrid)));
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(HierarchicalPairGrid), new PropertyMetadata(null));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ToggleGroupCommandProperty =
            DependencyProperty.Register("ToggleGroupCommand", typeof(ICommand), typeof(HierarchicalPairGrid), new PropertyMetadata(null));

        public ICommand ToggleGroupCommand
        {
            get { return (ICommand)GetValue(ToggleGroupCommandProperty); }
            set { SetValue(ToggleGroupCommandProperty, value); }
        }

        public static readonly DependencyProperty NumberIncrementCommandProperty =
            DependencyProperty.Register("NumberIncrementCommand", typeof(ICommand), typeof(HierarchicalPairGrid), new PropertyMetadata(null));

        public ICommand NumberIncrementCommand
        {
            get { return (ICommand)GetValue(NumberIncrementCommandProperty); }
            set { SetValue(NumberIncrementCommandProperty, value); }
        }

        public static readonly DependencyProperty NumberDecrementCommandProperty =
            DependencyProperty.Register("NumberDecrementCommand", typeof(ICommand), typeof(HierarchicalPairGrid), new PropertyMetadata(null));

        public ICommand NumberDecrementCommand
        {
            get { return (ICommand)GetValue(NumberDecrementCommandProperty); }
            set { SetValue(NumberDecrementCommandProperty, value); }
        }

        public static readonly DependencyProperty ColumnNameWidthProperty =
            DependencyProperty.Register("ColumnNameWidth", typeof(GridLength), typeof(HierarchicalPairGrid), new PropertyMetadata(new GridLength(100)));

        public GridLength ColumnNameWidth
        {
            get { return (GridLength)GetValue(ColumnNameWidthProperty); }
            set { SetValue(ColumnNameWidthProperty, value); }
        }

        public static readonly DependencyProperty IntegerIncrementStepProperty =
            DependencyProperty.Register("IntegerIncrementStep", typeof(int), typeof(HierarchicalPairGrid), new PropertyMetadata(1));

        public int IntegerIncrementStep
        {
            get { return (int)GetValue(IntegerIncrementStepProperty); }
            set { SetValue(IntegerIncrementStepProperty, value); }
        }

        public static readonly DependencyProperty FloatIncrementStepProperty =
            DependencyProperty.Register("FloatIncrementStep", typeof(float), typeof(HierarchicalPairGrid), new PropertyMetadata(1.0f));

        public float FloatIncrementStep
        {
            get { return (float)GetValue(FloatIncrementStepProperty); }
            set { SetValue(FloatIncrementStepProperty, value); }
        }

        public static readonly DependencyProperty DoubleIncrementStepProperty =
            DependencyProperty.Register("DoubleIncrementStep", typeof(double), typeof(HierarchicalPairGrid), new PropertyMetadata(1.0));

        public double DoubleIncrementStep
        {
            get { return (double)GetValue(DoubleIncrementStepProperty); }
            set { SetValue(DoubleIncrementStepProperty, value); }
        }

        public HierarchicalPairGrid()
        {
            ToggleGroupCommand = new ExecuteToggleGroup();
            NumberIncrementCommand = new ExecuteNumberIncrement() { Grid = this };
            NumberDecrementCommand = new ExecuteNumberDecrement() { Grid = this };
        }

        public class ExecuteToggleGroup : ICommand
        {
            public event EventHandler? CanExecuteChanged;
            public bool CanExecute(object? parameter)
            {
                return true;
            }
            public void Execute(object? parameter)
            {
                if (parameter is GroupItem groupItem)
                {
                    groupItem.IsExpanded = !groupItem.IsExpanded;
                }
            }
        }

        public class ExecuteNumberIncrement : ICommand
        {
            public HierarchicalPairGrid? Grid {  get; set; }

            public event EventHandler? CanExecuteChanged;
            public bool CanExecute(object? parameter)
            {
                return parameter is TypedItem<int> || parameter is TypedItem<float> || parameter is TypedItem<double>;
            }
            public void Execute(object? parameter)
            {
                if (parameter is TypedItem<int> intItem)
                {
                    intItem.Value += (Grid != null)? Grid.IntegerIncrementStep : 1;
                }
                else if (parameter is TypedItem<float> floatItem)
                {
                    floatItem.Value += (Grid != null) ? Grid.FloatIncrementStep : 1.0f;
                }
                else if (parameter is TypedItem<double> doubleItem)
                {
                    doubleItem.Value += (Grid != null) ? Grid.DoubleIncrementStep : 1.0;
                }
            }
        }

        public class ExecuteNumberDecrement : ICommand
        {
            public HierarchicalPairGrid? Grid { get; set; }

            public event EventHandler? CanExecuteChanged;
            public bool CanExecute(object? parameter)
            {
                return parameter is TypedItem<int> || parameter is TypedItem<float> || parameter is TypedItem<double>;
            }
            public void Execute(object? parameter)
            {
                if (parameter is TypedItem<int> intItem)
                {
                    intItem.Value -= (Grid != null) ? Grid.IntegerIncrementStep : 1;
                }
                else if (parameter is TypedItem<float> floatItem)
                {
                    floatItem.Value -= (Grid != null) ? Grid.FloatIncrementStep : 1.0f;
                }
                else if (parameter is TypedItem<double> doubleItem)
                {
                    doubleItem.Value -= (Grid != null) ? Grid.DoubleIncrementStep : 1.0;
                }
            }
        }
    }
}