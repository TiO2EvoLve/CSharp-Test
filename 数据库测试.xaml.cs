using System.Windows;
using Test.Tool;
using Test.ViewModels;

namespace Test;

public partial class 数据库测试 : Window
{
    
    public 数据库测试()
    {
        InitializeComponent();
    }
    
    List<User> Users = new();
    
    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        using (var context = new ApplicationDbContext())
        {
            // 查询所有学生
            Users = context.User.ToList();
        }
        UsersDataGrid.ItemsSource = Users;
    }
}