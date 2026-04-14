using System;
using System.Windows;
using System.Windows.Controls;
using Calculator.Scripts;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private TextBox _display;
        private string _currentInput = "";
        private double _lastValue = 0;
        private string _operation = "";

        public MainWindow()
        {
            InitializeComponent();
            BuildUI();
        }
        
        private void BuildUI()
        {
            Title = "Калькулятор";
            
            var grid = new Grid();

            for (int i = 0; i < 7; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 5; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            _display = new TextBox
            {
                FontSize = 24,
                IsReadOnly = true,
                Text = "0",
                Margin = new Thickness(5)
            };

            Grid.SetRow(_display, 0);
            Grid.SetColumnSpan(_display, 5);
            grid.Children.Add(_display);

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
                    Button btn;

                    if (char.IsDigit(content, 0) || content == ",")
                    {
                        btn = numberFactory.CreateButton(content);
                    }
                    else
                    {
                        btn = operationFactory.CreateButton(content);
                    }

                    btn.Click += OnButtonClick;

                    Grid.SetRow(btn, i + 1);
                    Grid.SetColumn(btn, j);

                    grid.Children.Add(btn);
                }
            }

            this.Content = grid;
        }
        
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string value = button.Content.ToString();

            if (char.IsDigit(value, 0) || value == ",")
            {
                if (value == "," && _currentInput.Contains(",")) 
                    return;
                _currentInput += value;
                _display.Text = _currentInput;
            }
            else if (value == "C")
            {
                Reset();
            }
            else if (value == "=")
            {
                Calculate();
                _operation = "";
            }
            else if (value == "+/-")
            {
                if (!string.IsNullOrEmpty(_currentInput))
                {
                    double val = double.Parse(_currentInput);
                    val = -val;
                    _currentInput = val.ToString();
                    _display.Text = _currentInput;
                }
            }
            else if (IsUnary(value))
            {
                ApplyUnary(value);
            }
            else
            {
                if (_currentInput != "")
                {
                    Calculate();
                }

                _operation = value;
                _currentInput = "";
            }
        }
        
        private bool IsUnary(string op)
        {
            return op == "sqrt" || op == "log" || op == "sin" || op == "cos" || op == "tan";
        }

        private void ApplyUnary(string operation)
        {
            double value = string.IsNullOrEmpty(_currentInput) ? _lastValue : double.Parse(_currentInput);
            switch (operation)
            {
                case "sqrt": 
                    value = Math.Sqrt(value); 
                    break;
                case "log": 
                    value = Math.Log10(value); 
                    break;
                case "sin": 
                    value = Math.Sin(value); 
                    break;
                case "cos": 
                    value = Math.Cos(value); 
                    break;
                case "tan": 
                    value = Math.Tan(value); 
                    break;
            }

            _currentInput = value.ToString();
            _display.Text = _currentInput;
        }

        private void Calculate()
        {
            double current = string.IsNullOrEmpty(_currentInput) ? 0 : double.Parse(_currentInput);

            switch (_operation)
            {
                case "+": 
                    _lastValue += current; 
                    break;
                case "-": 
                    _lastValue -= current; 
                    break;
                case "*": 
                    _lastValue *= current; 
                    break;
                case "/": 
                    _lastValue /= current; 
                    break;
                case "^": 
                    _lastValue = Math.Pow(_lastValue, current); 
                    break;
                case "%": 
                    _lastValue = _lastValue * current / 100.0; 
                    break;
                default: 
                    _lastValue = current; 
                    break;
            }

            _display.Text = _lastValue.ToString();
            _currentInput = "";
        }
        
        private void Reset()
        {
            _currentInput = "";
            _lastValue = 0;
            _operation = "";
            _display.Text = "0";
        }
    }
}