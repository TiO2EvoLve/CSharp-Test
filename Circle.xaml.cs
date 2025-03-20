using System.Windows;

namespace Test;

public partial class Circle : Window
{
    public Circle()
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