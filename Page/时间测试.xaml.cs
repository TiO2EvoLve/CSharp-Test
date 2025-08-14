using System.Windows;
using Test.ViewModels;

namespace Test;

public partial class 时间测试 : Window
{
    public 时间测试()
    {
        InitializeComponent();
    }


    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        DateTime? dateTime = TimePicker.SelectedDateTime;
        MessageBox.Show( day.Text + "天后的时间为：" + dateTime.Value.AddDays(double.Parse(day.Text)).ToString("yyyy-MM-dd"));
    }
}