using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using mmPg = MM.PairGrids;

namespace PairGridsTestApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindowSimplePairGridTest.xaml
    /// </summary>
    public partial class MainWindowSimplePairGridTest : Window
    {
        public class MyOptions : mmPg.OptionalTypedItem<string>
        {
            public MyOptions()
            {
                Options = new string[] { "Option 1", "Option 2", "Option 3" };
                Value = Options[0];
            }
        }

        public MainWindowSimplePairGridTest()
        {
            InitializeComponent();
            DataContext = this;

            Spg.ColumnNameWidth = new GridLength(200);
        }

        public ObservableCollection<mmPg.BasicItem> Ocol { get; set; } = new ObservableCollection<mmPg.BasicItem>
        {
            new mmPg.TypedItem<int> { Name = "Item 1.1", Description = "This is item 1.1", Value = 10, Indentation="  " },
            new mmPg.TypedItem<string> { Name = "Item 1.2", Description = "This is item 1.2", Value = "Hello",  Indentation="  " },
            new MyOptions { Name = "Item 1.3", Description = "This is item 1.3", Indentation="  " },
            new mmPg.TypedItem<bool> { Name = "Item 1.4", Description = "This is item 1.4", Value = true, Indentation="  " },
        };

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }
    }
}
