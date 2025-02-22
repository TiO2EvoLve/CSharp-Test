using System.Windows;
using System.Windows.Controls;
using Test.Tool;

namespace Test;

public partial class MdbTest : Window
{
    public MdbTest()
    {
        InitializeComponent();
    }

    private void MdbFileTest(object sender, RoutedEventArgs e)
    {
        string sql = "Select NUM From kahao";
        List<string> select = MdbTool.Select(sql);

        foreach (var item in select)
        {
            Console.WriteLine(item);
        }
    }
}