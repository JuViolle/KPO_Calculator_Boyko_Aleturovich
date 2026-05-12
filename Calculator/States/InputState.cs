using Calculator.ViewModels;

namespace Calculator.States;

public class InputState : ICalculatorState
{
    public void Handle(MainViewModel vm, string input)
    {
        if (char.IsDigit(input, 0) || input == ",")
        {
            if (input == "," && vm.CurrentInput.Contains(",")) return;
            vm.CurrentInput += input;
            vm.Display = vm.CurrentInput;
        }
        else
        {
            vm.ChangeState(new OperationState());
            vm.HandleInput(input);
        }
    }
}