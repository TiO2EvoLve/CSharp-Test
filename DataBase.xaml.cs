using System.Windows;
using Test.Tool;
using Test.ViewModels;

namespace Test;

public partial class DataBase : Window
{
    public DataBase()
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