using System.Windows;
using Test.Tool;

namespace Test;

public partial class Mdb测试 : Window
{
    public Mdb测试()
    {
        InitializeComponent();
    }

    private void MdbFileTest(object sender, RoutedEventArgs e)
    {
        var sql = "Select NUM From kahao";
        var select = MdbTool.Select(sql);

        foreach (var item in select) 
            Console.WriteLine(item);
    }
}