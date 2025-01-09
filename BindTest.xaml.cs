using System.Windows;
using System.Windows.Controls;

namespace Test;

public partial class BindTest : Window
{
    public BindTest()
    {
        InitializeComponent();
        DataContext = new Page2ViewModel();
    }
    private void Text_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = DataContext as Page2ViewModel;
        if (viewModel != null)
        {
            string inputValue = viewModel.Input;
            Text2.Text = inputValue;
            Text3.Text = inputValue;
        }
    }
}