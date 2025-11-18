using System.IO;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;

namespace Test;

public partial class 数据写入测试 : Window
{
    public 数据写入测试()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "文本文件|*.txt",
            Title = "保存文件"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            if (saveFileDialog.FileName == "") return;
            //写入文件
            using (var writer = new StreamWriter(saveFileDialog.FileName))
            {
                writer.WriteLine(new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd).Text);
            }

            MessageBox.Show("文件保存成功");
        }
    }
}