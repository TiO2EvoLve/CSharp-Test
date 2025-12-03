using System.Windows;
using WpfStateMachineDemo;

namespace Test.Page;

public partial class 状态机测试 : Window
{
    public 状态机测试()
    {
        InitializeComponent();
        DataContext = new BulbStateMachineViewModel();
    }
}