using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaktionslogik für MainWindowHierarchicalPairGridTest.xaml
    /// </summary>
    public partial class MainWindowHierarchicalPairGridTest : Window
    {
        public MainWindowHierarchicalPairGridTest()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class MyOptions : mmPg.OptionalTypedItem<string>
        {
            public MyOptions()
            {
                Options = new string[] { "Option 1", "Option 2", "Option 3" };
                Value = Options[0];
            }
        }


        public ObservableCollection<mmPg.BasicItem> Ocol { get; set; } = new ObservableCollection<mmPg.BasicItem>
        {
            new mmPg.GroupItem
            {
                Name = "Group 1",
                Description = "This is group 1",
                Indentation ="  ",
                SubItems =
                {
                    new mmPg.TypedItem<int> { Name = "Item 1.1", Description = "This is item 1.1", Value = 10, Indentation="  " },
                    new mmPg.TypedItem<string> { Name = "Item 1.2", Description = "This is item 1.2", Value = "Hello",  Indentation="  " },
                    new MyOptions { Name = "Item 1.3", Description = "This is item 1.3", Indentation="  " },
                    new mmPg.GroupItem
                    {
                        Name = "Group 1.4",
                        Description = "This is group 1.4",
                        Indentation = "    ",
                        SubItems =
                        {
                            new mmPg.TypedItem<bool> { Name = "Item 1.4.1", Description = "This is item 1.4.1", Value = true, Indentation = "    " },
                            new mmPg.TypedItem<int> { Name = "Item 1.4.2", Description = "This is item 1.4.2", Value = 77, Indentation = "    " },
                            new mmPg.GroupItem
                            {
                                Name = "Group 1.4.3",
                                Description = "This is group 1.4.3",
                                Indentation = "      ",
                                SubItems =
                                {
                                    new mmPg.TypedItem<bool> { Name = "Item 1.4.3.1", Description = "This is item 1.4.3.1", Value = true, Indentation = "      " },
                                    new mmPg.TypedItem<int> { Name = "Item 1.4.3.2", Description = "This is item 1.4.3.2", Value = 77, Indentation = "      " },
                                    new mmPg.TypedItem<int> { Name = "Item 1.4.3.3", Description = "This is item 1.4.3.3", Value = 77, Indentation = "      " }
                                }
                            },
                            new mmPg.TypedItem<int> { Name = "Item 1.4.4", Description = "This is item 1.4.4", Value = 77, Indentation = "   " }
                        }
                    },
                    new mmPg.TypedItem<int> { Name = "Item 1.5 (int)", Description = "This is item 1.5", Value = 10, Indentation="  " },
                    new mmPg.TypedItem<float> { Name = "Item 1.6 (float)", Description = "This is item 1.6", Value = 10, Indentation="  " },
                    new mmPg.TypedItem<double> { Name = "Item 1.7 (double)", Description = "This is item 1.7", Value = 10, Indentation="  " },
                    new mmPg.GroupItem
                    {
                        Name = "Group 1.8",
                        Description = "This is group 1.8",
                        Indentation = "   ",
                        SubItems =
                        {
                            new mmPg.TypedItem<bool> { Name = "Item 1.8.1", Description = "This is item 1.8.1", Value = true, Indentation = "   " },
                            new mmPg.TypedItem<int> { Name = "Item 1.8.2", Description = "This is item 1.8.2", Value = 77, Indentation = "   " },
                        }
                    },
                }
            },
        };
    }
}
