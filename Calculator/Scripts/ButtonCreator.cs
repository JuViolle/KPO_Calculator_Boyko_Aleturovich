using System.Windows.Controls;

namespace Calculator.Scripts;

public abstract class ButtonCreator
{
    public abstract Button CreateButton(string content);
}