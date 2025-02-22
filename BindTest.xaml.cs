
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Test;

//使用prism包实现双向绑定
public partial class BindTest
{
    public BindTest()
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