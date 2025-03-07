using System.Collections.ObjectModel;
using System.IO;

namespace Test.ViewModels;

public class FileSystemItem
{
    public string Name { get; set; } // 文件或文件夹名称
    public string FullPath { get; set; } // 完整路径
    public bool IsDirectory { get; set; } // 是否为文件夹
    public ObservableCollection<FileSystemItem> Children { get; set; } // 子节点

    public FileSystemItem()
    {
        Children = new ObservableCollection<FileSystemItem>();
    }

    // 加载子节点
    public void LoadChildren()
    {
        if (!IsDirectory) return;

        try
        {
            var directories = Directory.GetDirectories(FullPath);
            foreach (var dir in directories)
            {
                Children.Add(new FileSystemItem
                {
                    Name = Path.GetFileName(dir),
                    FullPath = dir,
                    IsDirectory = true
                });
            }

            var files = Directory.GetFiles(FullPath);
            foreach (var file in files)
            {
                Children.Add(new FileSystemItem
                {
                    Name = Path.GetFileName(file),
                    FullPath = file,
                    IsDirectory = false
                });
            }
        }
        catch (UnauthorizedAccessException)
        {
            // 处理无权限访问的文件夹
        }
    }
}