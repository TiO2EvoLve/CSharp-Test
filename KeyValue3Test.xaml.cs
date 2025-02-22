using System.Windows;
// using ValveResourceFormat.Serialization.KeyValues;

namespace Test;

public partial class KeyValue3Test
{
    public KeyValue3Test()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        // string projectDirectory = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent.Parent.FullName;
        // string filePath = System.IO.Path.Combine(projectDirectory, "../Files/Model.vmdl");
        //
        // var kv3File = KeyValues3.ParseKVFile(filePath);
        // //增加键值对  
        // KVValue kvValue = new KVValue(KVType.STRING, "Cube/test.png");
        // kv3File.Root.AddProperty("test",kvValue);
        // //根据键输出值
        // Console.WriteLine(kv3File.Root.Properties["test"].Value);
        // //修改
        // kv3File.Root.Properties["test"] = new KVValue(KVType.STRING, "Cube/test2.png");
        // //删除
        // Console.WriteLine(kv3File.Root.Properties.Remove("test"));
        // //查找
        // Console.WriteLine(kv3File.Root.Properties.ContainsKey("test"));
        
    }
    
    
}