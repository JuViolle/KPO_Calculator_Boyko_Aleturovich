using System.Windows.Controls;

namespace Calculator.Factories;

public abstract class ButtonCreator
{
    public abstract Button CreateButton(string content);
}