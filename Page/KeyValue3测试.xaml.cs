using System.IO;
using System.Windows;
using ValveKeyValue;
using ValveResourceFormat.Serialization.KeyValues;
using KVValue = ValveResourceFormat.Serialization.KeyValues.KVValue;

namespace Test;

public partial class KeyValue3测试
{
    public KeyValue3测试()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var currentDirectory = Environment.CurrentDirectory;
        var filePath = Path.Combine(currentDirectory, "Files", "test.vmdl");

        var kv3File = KeyValues3.ParseKVFile(filePath);
        //增加键值对  
        var kvValue = new KVValue(KVValueType.String, "Cube/test.png");
        kv3File.Root.AddProperty("test", kvValue);
        //根据键输出值
        Console.WriteLine(kv3File.Root.Properties["test"].Value);
        //修改
        kv3File.Root.Properties["test"] = new KVValue(KVValueType.String, "Cube/test2.png");
        //删除
        Console.WriteLine(kv3File.Root.Properties.Remove("test"));
        //查找
        Console.WriteLine(kv3File.Root.Properties.ContainsKey("test"));
    }
}