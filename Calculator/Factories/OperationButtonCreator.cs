using System.Windows;
using System.Windows.Controls;

namespace Calculator.Factories;

public class OperationButtonCreator : ButtonCreator
{
    public override Button CreateButton(string content)
    {
        return new Button
        {
            Content = content,
            FontSize = 18,
            Margin = new Thickness(5),
            Background = System.Windows.Media.Brushes.LightGray
        };
    }
}