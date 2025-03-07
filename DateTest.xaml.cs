using System.Windows;
using Test.ViewModels;

namespace Test;

public partial class DateTest : Window
{
    public DateTest()
    {
        InitializeComponent();
    }


    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        DateTime? dateTime = TimePicker.SelectedDateTime;
        MessageBox.Show( day.Text + "天后的时间为：" + dateTime.Value.AddDays(double.Parse(day.Text)).ToString("yyyy-MM-dd"));
    }
}