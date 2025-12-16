using System.ComponentModel;
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
    [ObservableProperty] private string number = "原始文字";

    // 属性设置后的分部方法
    partial void OnNumberChanged(string value)
    {
        Console.WriteLine($"StatusMessage 已更改为: {value}");
    }
    // 属性设置前的分部方法
    partial void OnNumberChanging(string value)
    {
        // 在属性改变前执行
        Console.WriteLine($"StatusMessage 即将更改为: {value}");
    }

    [RelayCommand]
    public void Button_Click()
    {
        Number = "现行文字";
    }
}