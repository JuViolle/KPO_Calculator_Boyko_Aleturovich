using Calculator.ViewModels;

namespace Calculator.States;

public class OperationState : ICalculatorState
{
    public void Handle(MainViewModel vm, string input)
    {
        vm.HandleOperation(input);
        vm.ChangeState(new InputState());
    }
}