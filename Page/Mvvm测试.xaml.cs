using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Test.Page;

public partial class Mvvm测试 : Window
{
    public Mvvm测试()
    {
        InitializeComponent();
        DataContext = new VM();
    }
    
}

public partial class VM : ObservableObject
{
    [ObservableProperty]
    private string number = "原始文字";
    
    [RelayCommand]
    public void Button_Click()
    {
        Number = "";
    }
}