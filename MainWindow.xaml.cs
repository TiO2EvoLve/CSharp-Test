
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using Test.Page;

namespace Test
{
    public partial class MainWindow : Window
    {
        // 窗口映射
        private readonly Dictionary<string, Func<Window>> _windowFactory;

        public MainWindow()
        {
            InitializeComponent();

            _windowFactory = new Dictionary<string, Func<Window>>
            {
                ["DeepSeek"] = () => new DeepSeek测试(),
                ["JSON"] = () => new Json测试(),
                ["KeyValue3"] = () => new KeyValue3测试(),
                ["LinQ"] = () => new LinQ测试(),
                ["Mdb"] = () => new Mdb测试(),
                ["Mvvm"] = () => new Mvvm测试(),
                ["Toml"] = () => new Toml格式测试(),
                ["VPK"] = () => new Vpk文件格式测试(),
                ["事务测试"] = () => new 事务测试(),
                ["二维码"] = () => new 二维码测试(),
                ["图像处理"] = () => new 图像处理测试(),
                ["圆形界面"] = () => new 圆形界面(),
                ["异步"] = () => new 异步测试(),
                ["数据写入"] = () => new 数据写入测试(),
                ["数据库"] = () => new 数据库测试(),
                ["文件树"] = () => new 文件树视图测试(),
                ["绑定"] = () => new 绑定测试(),
                ["Email"] = () => new 邮件发送测试(),
                ["Hover"] = () => new 鼠标悬停测试(),
                ["二进制转文本"] = () => new 二进制转文本(),
                ["Test"] = () => new 测试(),
                ["反射"] = () => new 反射测试(),
                ["进度条"] = () => new 进度条测试(),
                ["文件修改"] = () => new 文件修改测试(),
                ["响应式"] = () => new 响应式()
            };

            CreateButtons();
            
        }

        // 自动创建按钮
        private void CreateButtons()
        {
            foreach (var kvp in _windowFactory)
            {
                var btn = new Button
                {
                    Content = kvp.Key,  // 按钮文字
                    Tag = kvp.Key,      // 用于查找对应窗口
                    Height = 40,
                    Width = 80,
                    Margin = new Thickness(2)
                };
                btn.Click += OpenWindow;
                ButtonGrid.Children.Add(btn);
            }
        }

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                if (_windowFactory.TryGetValue(tag, out var createWindow))
                {
                    var window = createWindow();
                    window.Show();
                }
                else
                {
                    MessageBox.Show($"未找到窗口: {tag}");
                }
            }
        }
    }
}
