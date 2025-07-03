using System.IO;
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Test;

//使用newtonspft.json实现json序列化
public partial class Json测试
{
    public Json测试()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        string currentDirectory = Environment.CurrentDirectory;

        // 构建文件路径
        string filePath = Path.Combine(currentDirectory, "Files", "test.json");
        Console.WriteLine(filePath);

        if (!File.Exists(filePath)) return;

        string jsonContent = File.ReadAllText(filePath);
        // 解析 JSON 字符串为 JObject
        JObject jsonObj = JObject.Parse(jsonContent);
        // 查询：获取某个属性
        Console.WriteLine("Name: " + jsonObj["name"]);

        // 修改：修改属性值
        jsonObj["name"] = "TiO2";

        //添加新的属性
        jsonObj["country"] = "USA";

        //删除某个属性
        jsonObj.Remove("name");

        // 将 JObject 转换回 JSON 字符串
        string modifiedJson = jsonObj.ToString();
        Console.WriteLine(modifiedJson);
    }
}