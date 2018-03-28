using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Model
{
    public class Node : INotifyPropertyChanged
    {
        public Node()
        {
            Children = new ObservableCollection<Node>();
            Rule = new AndProductionRule();
            Title = "";
            Selected = false;

            PropertyChanged += (obj, e) =>
            {
                if (e.PropertyName == "DefinitionCoef")
                    UpdateParentCoef();
            };
        }
        
        public void UpdateParentCoef()
        {
            if (Parent != null)
                Parent.OnPropertyChanged("DefinitionCoef");
        }

        private Node parent;
        public Node Parent
        {
            get => parent;
            set
            {
                if (parent == value)
                    return;
                parent = value;
                OnPropertyChanged("Parent");
            }
        }

        private bool selected;
        public bool Selected
        {
            get { return IsNode || selected; }
            set
            {
                if (selected == value)
                    return;

                selected = value;
                OnPropertyChanged("Selected");
                OnPropertyChanged("DefinitionCoef");
            }
        }


        public bool HasParent { get => Parent != null; }

        private ObservableCollection<Node> children;
        public ObservableCollection<Node> Children
        {
            get => children;
            set
            {
                if (children == value)
                    return;
                children = value;

                Children.CollectionChanged += (obj, e) =>
                {
                    OnPropertyChanged("IsLeaf");
                    OnPropertyChanged("IsNode");
                    OnPropertyChanged("Selected");
                    OnPropertyChanged("DefinitionCoef");
                };
                OnPropertyChanged("Children");
                OnPropertyChanged("IsLeaf");
                OnPropertyChanged("IsNode");
                OnPropertyChanged("DefinitionCoef");
            }
        }

        public string title;
        public string Title
        {
            get => title;
            set
            {
                if (value == title)
                    return;
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public bool IsLeaf { get => Children.Count() == 0; }
        public bool IsNode { get => !IsLeaf; }
        public string RuleName
        {
            get
            {
                if (Rule is AndProductionRule)
                    return "Тип об'єднання: І";
                else if (Rule is OrProductionRule)
                    return "Тип об'єднання: Або";
                else //if (Rule is DivideProductionRule)
                    return "Тип об'єднання: Розділення";
            }
        }

        private double definitionCoef;
        public double DefinitionCoef
        {
            get => IsLeaf ? 
                definitionCoef :
                Rule.Execute(Children.ToList());
            set
            {
                if (value >= 100)
                    definitionCoef = 100;
                else if (value <= 0)
                    definitionCoef = 0;
                else
                    definitionCoef = value;
                OnPropertyChanged("DefinitionCoef");
            }
        }
        private IProductionRule rule;
        public IProductionRule Rule
        {
            get { return rule; }
            set
            {
                if (value == rule)
                        return;
                    rule = value;
                OnPropertyChanged("Rule");
                OnPropertyChanged("RuleName");
                OnPropertyChanged("DefinitionCoef");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
