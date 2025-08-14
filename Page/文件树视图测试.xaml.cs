using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SteamDatabase.ValvePak;
using Test.ViewModels;

namespace Test;

public partial class 文件树视图测试 : Window
{
    public 文件树视图测试()
    {
        InitializeComponent();
        LoadRootNodes();
    }
    
    private void LoadRootNodes()
    {
        // 加载根目录（例如 C:\）
        var rootDirectory = new FileSystemItem
        {
            Name = "C:",
            FullPath = @"C:\",
            IsDirectory = true
        };
        rootDirectory.LoadChildren(); // 加载初始子节点
        FileTree.Items.Add(rootDirectory);

        // 订阅展开事件
        FileTree.AddHandler(TreeViewItem.ExpandedEvent, new RoutedEventHandler(OnTreeViewItemExpanded));
    }

    private void OnTreeViewItemExpanded(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is TreeViewItem treeViewItem && treeViewItem.DataContext is FileSystemItem item)
        {
            if (item.Children.Count == 0 && item.IsDirectory)
            {
                // 动态加载子节点
                item.LoadChildren();
            }
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        //选择路径再显示其文件树
        var openFolderDialog = new OpenFolderDialog();
        if (openFolderDialog.ShowDialog() == true)
        {
            var rootDirectory = new FileSystemItem
            {
                Name = openFolderDialog.FolderName,
                FullPath = openFolderDialog.FolderName,
                IsDirectory = true
            };
            rootDirectory.LoadChildren(); // 加载初始子节点
            FileTree.Items.Clear();
            FileTree.Items.Add(rootDirectory);
        }
    }
}