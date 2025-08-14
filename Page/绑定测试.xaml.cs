
using System.Windows.Controls;

namespace Test;

//使用prism包实现双向绑定
public partial class 绑定测试
{
    public 绑定测试()
    {
        InitializeComponent();
        DataContext = new InputValue();
    }
    private void Text_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = DataContext as InputValue;
        if (viewModel != null)
        {
            Console.WriteLine(viewModel.Input);
        }
    }
    
}