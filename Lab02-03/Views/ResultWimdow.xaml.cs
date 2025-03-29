using System.Windows;

namespace Lab02_03
{
    public partial class ResultsWindow : Window
    {
        public ResultsWindow(string resultText)
        {
            InitializeComponent();
            DataContext = resultText;
        }
    }
}