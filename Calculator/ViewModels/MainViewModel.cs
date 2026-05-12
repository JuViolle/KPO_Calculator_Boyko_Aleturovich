using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calculator.Commands;

namespace Calculator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public string Display
    {
        get => _display;
        set
        {
            _display = value;
            OnPropertyChanged();
        }
    }

    public ICommand ButtonCommand { get; }
    
    private string _display = "0";
    private string _currentInput = "";
    private double _lastValue = 0;
    private string _operation = "";
    
    private readonly Models.Calculator _calculatorModel;

    public MainViewModel(Models.Calculator calculator)
    {
        _calculatorModel = calculator;
        
        ButtonCommand = new RelayCommand(OnButtonClick);
    }

    private void OnButtonClick(object parameter)
    {
        string value = parameter.ToString();

        if (char.IsDigit(value, 0) || value == ",")
        {
            HandleNumber(value);
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
            ChangeSign();
        }
        else if (IsUnary(value))
        {
            ApplyUnary(value);
        }
        else
        {
            HandleOperation(value);
        }
    }

    private void HandleNumber(string value)
    {
        if (value == "," && _currentInput.Contains(","))
            return;

        _currentInput += value;
        Display = _currentInput;
    }

    private void HandleOperation(string value)
    {
        if (_currentInput != "")
        {
            Calculate();
        }

        _operation = value;
        _currentInput = "";
    }

    private void ChangeSign()
    {
        if (!string.IsNullOrEmpty(_currentInput))
        {
            double val = double.Parse(_currentInput);
            val = -val;

            _currentInput = val.ToString();
            Display = _currentInput;
        }
    }

    private bool IsUnary(string op)
    {
        return op == "sqrt" ||
               op == "log" ||
               op == "sin" ||
               op == "cos" ||
               op == "tan";
    }

    private void ApplyUnary(string operation)
    {
        double value = string.IsNullOrEmpty(_currentInput)
            ? _lastValue
            : double.Parse(_currentInput);

        value = _calculatorModel.ApplyUnary(value, operation);

        _currentInput = value.ToString();
        Display = _currentInput;
    }

    private void Calculate()
    {
        double current = string.IsNullOrEmpty(_currentInput)
            ? 0
            : double.Parse(_currentInput);

        _lastValue = _calculatorModel.Calculate(_lastValue, current, _operation);

        Display = _lastValue.ToString();
        _currentInput = "";
    }

    private void Reset()
    {
        _currentInput = "";
        _lastValue = 0;
        _operation = "";
        Display = "0";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}