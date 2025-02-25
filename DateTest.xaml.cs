using System.Windows;
using Test.ViewModels;

namespace Test;

public partial class DateTest : Window
{
    public DateTest()
    {
        InitializeComponent();
        DataContext = new Date();
    }
    
    
    
    
}