
using System.Windows.Controls;

namespace Test;

//使用prism包实现双向绑定
public partial class BindTest
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