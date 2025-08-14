using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Test;

public partial class Mvvm测试 : Window
{
    public Mvvm测试()
    {
        InitializeComponent();
        DataContext = new ViewModel();
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        (DataContext as ViewModel).Number = "新文字";
    }
}

public partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    private string number = "原始文字";
}