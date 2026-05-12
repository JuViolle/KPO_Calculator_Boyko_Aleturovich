using Calculator.ViewModels;
using Calculator.Views;

namespace Calculator
{
    public partial class App
    {
        public App()
        {
            var calculator = new Models.Calculator();
            var mainViewModel = new MainViewModel(calculator);
            var mainWindow = new MainWindow(mainViewModel);
            mainWindow.Show();
        }
    }
}