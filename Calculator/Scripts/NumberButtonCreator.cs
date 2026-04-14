using System.Windows;
using System.Windows.Controls;

namespace Calculator.Scripts;

public class NumberButtonCreator : ButtonCreator
{
    public override Button CreateButton(string content)
    {
        return new Button
        {
            Content = content,
            FontSize = 18,
            Margin = new Thickness(5)
        };
    }
}