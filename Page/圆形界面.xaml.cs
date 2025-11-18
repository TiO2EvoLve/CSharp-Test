using System.Windows;
using System.Windows.Input;

namespace Test;

public partial class 圆形界面 : Window
{
    public 圆形界面()
    {
        InitializeComponent();
    }

    private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}