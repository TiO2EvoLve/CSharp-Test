using System.Windows;
using System.Windows.Controls;
using Test.ViewModels;

namespace Test;

public partial class FileTreeTest : Window
{
    public FileTreeTest()
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
}