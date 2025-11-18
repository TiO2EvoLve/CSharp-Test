using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace Test;

public partial class 二进制转文本 : Window
{
    public 二进制转文本()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        // 打开文件选择器
        var openFileDialog = new OpenFileDialog
        {
            Title = "选择二进制文件"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            var base64String = ConvertBinaryToBase64(openFileDialog.FileName);

            // 获取桌面路径
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // 定义输出文件路径
            var outputFilePath = Path.Combine(desktopPath, "output.txt");

            // 将 Base64 字符串写入文件
            File.WriteAllText(outputFilePath, base64String);

            MessageBox.Show($"文件已成功转换并保存到 {outputFilePath}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public static string ConvertBinaryToBase64(string filePath)
    {
        // 读取二进制文件内容为字节数组
        var fileBytes = File.ReadAllBytes(filePath);

        // 将字节数组转换为 Base64 字符串
        var base64String = Convert.ToBase64String(fileBytes);

        return base64String;
    }
}