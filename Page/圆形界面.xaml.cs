using System.Windows;

namespace Test;

public partial class 圆形界面 : Window
{
    public 圆形界面()
    {
        InitializeComponent();
    }
    
    private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }
    
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}