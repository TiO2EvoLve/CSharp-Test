using System.Windows;


namespace Test;

public partial class 进度条测试
{
     private CancellationTokenSource _cancellationTokenSource;

        public 进度条测试()
        {
            InitializeComponent();
            ProgressBar.Value = 0;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // 重置UI状态
            StartButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            ProgressBar.Value = 0;
            StatusText.Text = "正在初始化...";
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                // 创建进度报告器
                var progress = new Progress<int>(percent =>
                {
                    // 此回调在UI线程自动执行
                    ProgressBar.Value = percent;
                    StatusText.Text = $"{percent}% 已完成";
                });

                // 在后台线程执行耗时操作
                await Task.Run(() => ProcessData(progress, _cancellationTokenSource.Token), 
                              _cancellationTokenSource.Token);
                
                StatusText.Text = "处理完成！";
            }
            catch (OperationCanceledException)
            {
                StatusText.Text = "操作已取消";
                ProgressBar.Value = 0;
            }
            catch (Exception ex)
            {
                StatusText.Text = $"错误: {ex.Message}";
            }
            finally
            {
                StartButton.IsEnabled = true;
                CancelButton.IsEnabled = false;
                _cancellationTokenSource?.Dispose();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            CancelButton.IsEnabled = false;
        }

        // 模拟耗时操作 - 在实际应用中替换为真实逻辑
        private void ProcessData(IProgress<int> progress, CancellationToken token)
        {
            int totalSteps = 100;
            
            for (int i = 0; i <= totalSteps; i++)
            {
                // 检查取消请求
                token.ThrowIfCancellationRequested();
                
                // 模拟工作 - 替换为真实操作
                Thread.Sleep(50);
                
                // 计算进度百分比
                int percentComplete = (int)((double)i / totalSteps * 100);
                
                // 报告进度（仅在有变化时报告，减少UI更新频率）
                progress?.Report(percentComplete);
                
                // 对于长时间操作，可以添加延迟报告逻辑优化性能
                if (i % 10 == 0) progress?.Report(percentComplete);
            }
        }
}