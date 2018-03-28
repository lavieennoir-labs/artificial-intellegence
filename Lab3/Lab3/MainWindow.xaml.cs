using Lab3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Node Root { get; set; }

        public MainWindow()
        {
            Root = new Node
            {
                Rule = new AndProductionRule()
            };
            var DataNode = 
                new Node
                {
                    Rule = new DivideProductionRule(),
                    Parent = Root
                };
            var PropertyNode =
                new Node
                {
                    Rule = new OrProductionRule(),
                    Parent = Root
                };
            Root.Children.Add(DataNode);
            Root.Children.Add(PropertyNode);
            var LeafData = new ObservableCollection<Node>
            {
                new Node
                {
                    Title = "Окремий монітор",
                    DefinitionCoef = 100,
                    Parent = DataNode,
                    Selected = true                    
                },
                new Node
                {
                    Title = "Суміщений монітор",
                    DefinitionCoef = 10,
                    Parent = DataNode
                },
                new Node
                {
                    Title = "Середні габарити",
                    DefinitionCoef = 50,
                    Parent = DataNode,
                    Selected = true
                },
                new Node
                {
                    Title = "Малі габарити",
                    DefinitionCoef = 10,
                    Parent = DataNode
                },
                new Node
                {
                    Title = "Велика вага",
                    DefinitionCoef = 90,
                    Parent = DataNode,
                    Selected = true
                },
                new Node
                {
                    Title = "Мала вага",
                    DefinitionCoef = 10,
                    Parent = DataNode
                }
            };
            var LeafProperties = new ObservableCollection<Node>
            {
                new Node
                {
                    Title = "Автономне живлення",
                    DefinitionCoef = 10,
                    Parent = PropertyNode
                },
                new Node
                {
                    Title = "Живлення від мережі",
                    DefinitionCoef = 100,
                    Parent = PropertyNode,
                    Selected = true
                }
            };
            DataNode.Children = LeafData;
            PropertyNode.Children = LeafProperties;
            InitializeComponent();
            treeView.Items.Add(Root);
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedNode = treeView.SelectedItem as Node;

            if (selectedNode.IsLeaf)
                treeView.ContextMenu = treeView.Resources["LeafContext"] as ContextMenu;
            else
            {
                treeView.ContextMenu = treeView.Resources["NodeContext"] as ContextMenu;

                foreach (var item in treeView.ContextMenu.Items)
                {
                    var menuItem = item as MenuItem;
                    if (menuItem.Header.ToString() == "Видалити вузол")
                    {
                        if (selectedNode.HasParent)
                            menuItem.Visibility = Visibility.Visible;
                        else
                            menuItem.Visibility = Visibility.Collapsed;
                    }
                    else if (menuItem.Header.ToString() == "Змінити правило")
                    {
                        foreach (var i in menuItem.Items)
                            if (i is RadioButton rb)
                                switch (rb.Content.ToString())
                                {
                                    case "І":
                                        rb.IsChecked = selectedNode.Rule is AndProductionRule;
                                        break;
                                    case "Або":
                                        rb.IsChecked = selectedNode.Rule is OrProductionRule;
                                        break;
                                    case "Розділення":
                                        rb.IsChecked = selectedNode.Rule is DivideProductionRule;
                                        break;
                                }
                        break;
                    }
                }
            }
        }
        
        private void AddLeaf_Click(object sender, RoutedEventArgs e)
        {
            var selectedNode = treeView.SelectedItem as Node;
            selectedNode.Children.Add(
                new Node
                {
                    Title = "Вузол",
                    DefinitionCoef = 0,
                    Parent = selectedNode
                });
        }
        private void RemoveLeaf_Click(object sender, RoutedEventArgs e)
        {
            var selectedNode = treeView.SelectedItem as Node;
            if (selectedNode.Parent == null)
                return;
            selectedNode.Parent.Children.Remove(selectedNode);
        }

        private void MenuRadioItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedNode = treeView.SelectedItem as Node;
            var menuItemRadio = sender as RadioButton;
            menuItemRadio.IsChecked = true;

            switch(menuItemRadio.Content.ToString())
            {
                case "І":
                    selectedNode.Rule = new AndProductionRule();
                    break;
                case "Або":
                    selectedNode.Rule = new OrProductionRule();
                    break;
                case "Розділення":
                    selectedNode.Rule = new DivideProductionRule();
                    break;
            }
        }
    }
}
