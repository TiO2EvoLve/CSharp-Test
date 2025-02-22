using System.Windows;
using System.Windows.Controls;
using Test.Tool;

namespace Test;

public partial class MdbTest : Page
{
    public MdbTest()
    {
        InitializeComponent();
    }

    private void MdbFileTest(object sender, RoutedEventArgs e)
    {
        string sql = "Select SUM From kahao";
        var select = MdbTool.Select(sql);

        foreach (var item in select)
        {
            Console.WriteLine(item);
        }
    }
}