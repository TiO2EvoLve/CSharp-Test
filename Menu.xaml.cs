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
                new DeepSeek测试().Show(); 
                break;
            case "JSON":
                new Json测试().Show(); 
                break;
            case "KeyValue3":
                new KeyValue3测试().Show(); 
                break;
            case "LinQ测试":
                new LinQ测试().Show(); 
                break;
            case "Mdb":
                new Mdb测试().Show(); 
                break;
            case "Mvvm":
                new Mvvm测试().Show(); 
                break; 
            case "Toml":
                new Toml格式测试().Show(); 
                break;
            case "VPK":
                new Vpk文件格式测试().Show(); 
                break;
            case "事务测试":
                new 事务测试().Show(); 
                break;
            case "二维码":
                new 二维码测试().Show(); 
                break;
            case "图像处理":
                new 图像处理测试().Show(); 
                break;
            case "圆形界面":
                new 圆形界面().Show(); 
                break;
            case "异步":
                new 异步测试().Show(); 
                break;
            case "数据写入":
                new 数据写入测试().Show(); 
                break;
            case "数据库":
                new 数据库测试().Show(); 
                break;
            case "文件树":
                new 文件树视图测试().Show(); 
                break;
            case "时间":
                new 时间测试().Show(); 
                break;
            case "绑定":
                new 绑定测试().Show(); 
                break;
            case "设计模式":
                new 设计模式测试().Show(); 
                break;
            case "Email":
                new 邮件发送测试().Show(); 
                break;
            case "Hover":
                new 鼠标悬停测试().Show(); 
                break;
            case "二进制转文本":
                new 二进制转文本().Show(); 
                break;
            case "Test":
                new 测试().Show(); 
                break;
            case "反射":
                new 反射测试().Show(); 
                break;
            
            default:
                MessageBox.Show($"未找到窗口: {windowName}");
                break;
        }
    }
}