using Calculator.ViewModels;

namespace Calculator.States;

public interface ICalculatorState
{
    void Handle(MainViewModel vm, string input);
}