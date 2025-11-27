using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using SteamDatabase.ValvePak;
using ValveResourceFormat;
using ValveResourceFormat.IO;
using ValveResourceFormat.ResourceTypes;

namespace Test;

//需要.net 9
public partial class Vpk文件格式测试
{
    public Vpk文件格式测试()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        string file;
        //打开文件选择对话框
        var openFileDialog = new OpenFileDialog
        {
            Filter = "vpk文件|*.vpk",
            Title = "选择vpk文件"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            if (openFileDialog.FileName == "") return;
            //读取文件内容
            file = openFileDialog.FileName;
        }
        else
        {
            return;
        }

        using var package = new Package();

        // 打开vpk文件
        package.Read(file);

        // Can also pass in a stream
        //package.Read(File.OpenRead(file));

        // 可选地验证文件的哈希值和签名（如果有的话）
        package.VerifyHashes();

        // 查找文件，这将返回一个PackageEntry
        var files = package.FindEntry("materials/floors/cobblestone_001_color_psd_327322ec.vtex_c");


        if (files != null)
        {
            // 将文件读入字节数组
            package.ReadEntry(files, out var fileContents);

            using var resource = new Resource();
            resource.Read(new MemoryStream(fileContents));

            //反编译为png图片
            using var bitmap = ((Texture)resource.DataBlock).GenerateBitmap();
            var bytes = TextureExtract.ToPngImage(bitmap);
            // 保存为png图片到桌面
            // 将字节数组转换为图片
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var outputPath = Path.Combine(desktopPath, "output.png");
            File.WriteAllBytes(outputPath, bytes);
            

            Console.WriteLine("成功");
        }
        else
        {
            Console.WriteLine("未找到文件");
        }
    }
}