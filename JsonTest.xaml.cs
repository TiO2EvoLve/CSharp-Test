
using System.Windows;
using Newtonsoft.Json.Linq;

namespace Test;

//使用newtonspft.json实现json序列化
public partial class JsonTest
{
    public JsonTest()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        string json;
        //打开文件选择对话框
         Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
         {
             Filter = "json文件|*.json",
             Title = "选择json文件"
         };
         if (openFileDialog.ShowDialog() == true)
         {
             if (openFileDialog.FileName == "") return;
             //读取文件内容
             json = System.IO.File.ReadAllText(openFileDialog.FileName);
         }else return;
         
        // 解析 JSON 字符串为 JObject
        JObject jsonObj = JObject.Parse(json);

        // 查询：获取某个属性
        Console.WriteLine("Name: " + jsonObj["project"]?["name"]);
        
        // 修改：修改属性值
        jsonObj["project"]!["name"] = "TiO2";
                            
        //添加新的属性
        jsonObj["project"]!["country"] = "USA";
            
        //删除某个属性
        //jsonObj["project"]["name"].Parent.Remove();
            
        // 将 JObject 转换回 JSON 字符串
        string modifiedJson = jsonObj.ToString();
        Console.WriteLine(modifiedJson);
    }
}