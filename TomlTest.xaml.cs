using System.IO;
using System.Windows;
using Tommy;

namespace Test;

public partial class TomlTest : Window
{
    public TomlTest()
    {
        InitializeComponent();
    }

    private void TomlFileTest(object sender, RoutedEventArgs e)
    {
        string configPath = "Files/tomltest.toml";
        
        TextReader tomlText = new StreamReader(configPath);
            
        var table = TOML.Parse(tomlText);

        // 访问解析后的数据
        Console.WriteLine((string)table["青岛"]["citycard"]); 
           
        //遍历toml文件
        TraverseTomlTable(table);
        
    }
    void TraverseTomlTable(TomlTable table, string prefix = "")
    {
        foreach (var key in table.Keys)
        {
            var value = table[key];

            // 如果值是嵌套的表，递归遍历
            if (value is TomlTable nestedTable)
            {
                Console.WriteLine($"{prefix}[{key}]");
                TraverseTomlTable(nestedTable, prefix + "  ");
            }
            else
            {
                // 输出键值对
                Console.WriteLine($"{prefix}{key} = {value}");
            }
        }
    }
}