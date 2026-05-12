using Calculator.ViewModels;

namespace Calculator.States;

public class ResultState : ICalculatorState
{
    public void Handle(MainViewModel vm, string input)
    {
        vm.Reset();
        vm.ChangeState(new InputState());
        vm.HandleInput(input);
    }
}