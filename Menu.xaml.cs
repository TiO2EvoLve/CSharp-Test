using System.Windows;
using System.Windows.Controls;

namespace Test;

public partial class Menu : Window
{
    public Menu()
    {
        InitializeComponent();
    }

    private void OpenWindow(object sender, RoutedEventArgs e)
    {
        Button button = (Button)sender;
        string windowName = button.Tag.ToString(); // 获取Tag中的窗口名称

        // 根据名称打开对应窗口
        switch (windowName)
        {
            case "DeepSeek":
                new DeepSeek测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "JSON":
                new Json测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "KeyValue3":
                new KeyValue3测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "LinQ测试":
                new LinQ测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "Mdb":
                new Mdb测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "Mvvm":
                new Mvvm测试().Show(); // 假设WindowA是已定义的窗口类
                break; 
            case "Toml":
                new Toml格式测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "VPK":
                new Vpk文件格式测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "事务测试":
                new 事务测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "二维码":
                new 二维码测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "图像处理":
                new 图像处理测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "圆形界面":
                new 圆形界面().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "异步":
                new 异步测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "数据写入":
                new 数据写入测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "数据库":
                new 数据库测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "文件树":
                new 文件树视图测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "时间":
                new 时间测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "绑定":
                new 绑定测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "设计模式":
                new 设计模式测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "Email":
                new 邮件发送测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            case "Hover":
                new 鼠标悬停测试().Show(); // 假设WindowA是已定义的窗口类
                break;
            
            default:
                MessageBox.Show($"未找到窗口: {windowName}");
                break;
        }
    }
}