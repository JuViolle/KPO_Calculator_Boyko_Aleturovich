using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Calculator.Factories;
using Calculator.ViewModels;

namespace Calculator.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = viewModel;
            
            BuildUI();
        }
        
        private void BuildUI()
        {
            Title = "Калькулятор";
            
            var grid = new Grid()
            {
                Margin = new Thickness(10)
            };

            for (int i = 0; i < 7; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 5; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            var display = new TextBox
            {
                FontSize = 24,
                IsReadOnly = true,
                Margin = new Thickness(5),
                Height = 50
            };

            display.SetBinding(TextBox.TextProperty, new Binding("Display"));

            Grid.SetRow(display, 0);
            Grid.SetColumnSpan(display, 5);
            grid.Children.Add(display);

            string[,] buttons =
            {
                {"sin", "cos", "tan", "sqrt", "log"},
                {"7", "8", "9", "/", "^"},
                {"4", "5", "6", "*", "%"},
                {"1", "2", "3", "-", "+/-"},
                {"0", ",", "=", "+", "C"}
            };

            ButtonCreator numberFactory = new NumberButtonCreator();
            ButtonCreator operationFactory = new OperationButtonCreator();
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    string content = buttons[i, j];

                    Button button;
                    if (char.IsDigit(content, 0) || content == ",")
                    {
                        button = numberFactory.CreateButton(content);
                    }
                    else
                    {
                        button = operationFactory.CreateButton(content);
                    }

                    button.SetBinding(Button.CommandProperty,
                        new Binding("ButtonCommand"));

                    button.CommandParameter = content;

                    Grid.SetRow(button, i + 1);
                    Grid.SetColumn(button, j);

                    grid.Children.Add(button);
                }
            }

            Content = grid;
        }
    }
}