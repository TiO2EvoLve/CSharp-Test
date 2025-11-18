using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Test;

public partial class 图像处理测试 : Window
{
    private BitmapImage originalImage;

    public 图像处理测试()
    {
        InitializeComponent();
    }

    private void SelectImageButton_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "图像文件 (*.jpg;*.png)|*.jpg;*.png";
        if (openFileDialog.ShowDialog() == true)
        {
            originalImage = new BitmapImage(new Uri(openFileDialog.FileName));
            DisplayImage.Source = originalImage;
        }
    }

    private void AddWatermarkButton_Click(object sender, RoutedEventArgs e)
    {
        if (originalImage != null)
        {
            // 创建一个DrawingVisual对象用于绘制水印
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                // 绘制原始图像
                drawingContext.DrawImage(originalImage, new Rect(0, 0, originalImage.Width, originalImage.Height));

                // 创建水印文本
                var watermarkText = new FormattedText(
                    "TiO2",
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Arial"),
                    36,
                    Brushes.White,
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);

                // 计算水印位置
                var x = originalImage.Width - watermarkText.Width - 10;
                var y = originalImage.Height - watermarkText.Height - 10;

                // 绘制水印
                drawingContext.DrawText(watermarkText, new Point(x, y));
            }

            // 创建一个RenderTargetBitmap对象用于将DrawingVisual渲染为图像
            var renderTargetBitmap = new RenderTargetBitmap(
                (int)originalImage.Width,
                (int)originalImage.Height,
                96,
                96,
                PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);

            // 将RenderTargetBitmap设置为Image控件的源
            DisplayImage.Source = renderTargetBitmap;
        }
    }

    private void SaveImageButton_Click(object sender, RoutedEventArgs e)
    {
        if (DisplayImage.Source != null)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG图像 (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)DisplayImage.Source));
                using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }

                MessageBox.Show("图像保存成功！");
            }
        }
    }
}