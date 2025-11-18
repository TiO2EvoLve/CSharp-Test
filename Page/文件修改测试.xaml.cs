using System.Diagnostics;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Test.ViewModels;

namespace Test;

public partial class 文件修改测试
{
    public 文件修改测试()
    {
        InitializeComponent();
        DataContext = new ViewModel();
        GetData();
    }

    private void GetData()
    {
        var currentDirectory = Environment.CurrentDirectory;
        // 构建文件路径
        var filePath = Path.Combine(currentDirectory, "Files", "config", "config.json");
        Console.WriteLine($"读取文件路径：{filePath}");
        if (!File.Exists(filePath))
        {
            MessageBox.Show("配置文件不存在，请检查路径。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var jsonContent = File.ReadAllText(filePath);
        // 解析 JSON 字符串为 JObject
        var jsonObj = JObject.Parse(jsonContent);
        //解析为对象
        var data = jsonObj.ToObject<SoundConfig>();
        if (data != null)
        {
            (DataContext as ViewModel)!.Ui = data.UI;
            (DataContext as ViewModel)!.Volume = data.Volume;
            (DataContext as ViewModel)!.Pitch = data.Pitch;
            (DataContext as ViewModel)!.Loop = data.Loop;
            (DataContext as ViewModel)!.Mode = data.Mode;
            (DataContext as ViewModel)!.Sounds = data.Sounds != null ? string.Join(",", data.Sounds) : string.Empty;
        }
    }

    private void SaveFile(object sender, RoutedEventArgs e)
    {
        //保存文件
        // 获取 ViewModel
        var vm = DataContext as ViewModel;
        if (vm == null)
        {
            MessageBox.Show("找不到 ViewModel。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // 构建 SoundConfig 对象
        var config = new SoundConfig
        {
            UI = vm.Ui,
            Volume = vm.Volume,
            Pitch = vm.Pitch,
            Loop = vm.Loop,
            Mode = vm.Mode,
            Sounds = string.IsNullOrWhiteSpace(vm.Sounds)
                ? new List<string>()
                : vm.Sounds.Split(',').Select(s => s.Trim()).ToList()
        };

        // 序列化为 JSON 字符串（带缩进）
        var json = JsonConvert.SerializeObject(config, Formatting.Indented);

        // 路径与读取时保持一致
        var currentDirectory = Environment.CurrentDirectory;
        var filePath = Path.Combine(currentDirectory, "Files", "config", "config.json");
        Console.WriteLine("保存路径：" + filePath);
        try
        {
            File.WriteAllText(filePath, json);
            MessageBox.Show("保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("保存失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SelectFile(object sender, RoutedEventArgs e)
    {
        // 打开文件选择对话框
        var openFileDialog = new OpenFileDialog
        {
            Filter = "All Files (*.*)|*.*"
        };

        if (openFileDialog.ShowDialog() == true) (DataContext as ViewModel)!.Sounds = openFileDialog.FileName;
    }

    private void OpenFile(object sender, RoutedEventArgs e)
    {
        var currentDirectory = Environment.CurrentDirectory;
        var filePath = Path.Combine(currentDirectory, "Files", "config", "config.json");
        if (File.Exists(filePath))
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // 使用系统默认程序打开文件
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
    }
}

public partial class ViewModel : ObservableObject
{
    [ObservableProperty] private bool loop;

    [ObservableProperty] private string mode;

    [ObservableProperty] private string pitch;

    [ObservableProperty] private string sounds;

    [ObservableProperty] private bool ui;

    [ObservableProperty] private string volume;

    public List<string> Modes { get; } =
    [
        "Random",
        "Once",
        "Order"
    ];
}