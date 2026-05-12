using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calculator.Commands;
using Calculator.States;

namespace Calculator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly Models.Calculator _calculatorModel;

    public ObservableCollection<string> History { get; } = new();

    private ICalculatorState _state;

    private string _display = "0";
    public string CurrentInput = "";
    private double _lastValue = 0;
    private string _operation = "";

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

    public MainViewModel(Models.Calculator calculator)
    {
        _calculatorModel = calculator;

        ButtonCommand = new RelayCommand(OnButtonClick);
        _state = new InputState();
    }

    private void OnButtonClick(object parameter)
    {
        string value = parameter.ToString();
        HandleInput(value);
    }

    public void HandleInput(string value)
    {
        _state.Handle(this, value);
    }

    public void ChangeState(ICalculatorState state)
    {
        _state = state;
    }

    public void HandleOperation(string value)
    {
        if (value == "C")
        {
            Reset();
            return;
        }

        if (value == "=")
        {
            Calculate();
            _operation = "";
            ChangeState(new ResultState());
            return;
        }

        _operation = value;
        _lastValue = string.IsNullOrEmpty(CurrentInput) ? 0 : double.Parse(CurrentInput);
        CurrentInput = "";
    }

    private void Calculate()
    {
        double current = string.IsNullOrEmpty(CurrentInput) ? 0 : double.Parse(CurrentInput);

        double result = _calculatorModel.Calculate(_lastValue, current, _operation);

        string record = $"{_lastValue} {_operation} {current} = {result}";
        History.Add(record);

        Display = result.ToString();
        CurrentInput = "";
        _lastValue = result;
    }

    public void Reset()
    {
        CurrentInput = "";
        _lastValue = 0;
        _operation = "";
        Display = "0";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}