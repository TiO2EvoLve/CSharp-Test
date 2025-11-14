using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Test.Page;

public partial class 响应式
{
    private readonly string[] _data = new[]
    {
        "apple", "applet", "application", "banana", "blueberry", "blackberry",
        "cherry", "cranberry", "grape", "grapefruit", "melon", "mango", "orange",
        "pear", "peach", "plum", "pineapple", "strawberry", "watermelon"
    };

    public 响应式()
    {
        InitializeComponent();
        StartButton.Click += StartButton_Click;
        StartButton1.Click += StartButton_Click1;

        // 监听 TextBox 输入
        Observable
            .FromEventPattern<TextChangedEventArgs>(SearchBox, nameof(SearchBox.TextChanged))
            .Select(_ => SearchBox.Text) // 获取文本
            .Throttle(TimeSpan.FromMilliseconds(400)) // 输入防抖
            .DistinctUntilChanged() // 相同输入不重复触发
            .ObserveOn(SynchronizationContext.Current!) // 回到UI线程
            .Subscribe(query =>
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    ResultList.ItemsSource = null;
                    return;
                }

                var results = _data.Where(x => x.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
                ResultList.ItemsSource = results;
            });

        ////////////////////////////////
        // 每个按钮一个流
        var aClicks = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => ButtonA.Click += h,
                h => ButtonA.Click -= h)
            .Select(_ => "A");

        var bClicks = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => ButtonB.Click += h,
                h => ButtonB.Click -= h)
            .Select(_ => "B");

        var cClicks = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => ButtonC.Click += h,
                h => ButtonC.Click -= h)
            .Select(_ => "C");

        var merged = Observable.Merge(aClicks, bClicks, cClicks)
            .Timestamp();

        // 订阅并写日志
        merged.ObserveOn(SynchronizationContext.Current!).Subscribe(e =>
        {
            LogBox.AppendText($"[{e.Timestamp:T}] 按钮 {e.Value} 被点击\n");
            LogBox.ScrollToEnd();
        });
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        const int totalSeconds = 10;

        // 创建一个每秒触发的流
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Take(totalSeconds + 1) // 10 秒 + 0 秒（显示起点）
            .Select(i => totalSeconds - (int)i) // 计算剩余秒数
            .ObserveOn(SynchronizationContext.Current!)
            .Subscribe(
                remaining => { TimerText.Text = $"剩余 {remaining} 秒"; },
                () => { TimerText.Text = "倒计时结束！"; });
    }

    private void StartButton_Click1(object sender, RoutedEventArgs e)
    {
        LogBox1.Clear();

        var random = new Random();
        int attemptCount = 0;
        const int maxAttempts = 5;
        bool hasSuccess = false;

        Observable.Interval(TimeSpan.FromSeconds(1))
            .ObserveOn(SynchronizationContext.Current!)
            .TakeWhile(_ => !hasSuccess && attemptCount < maxAttempts)
            .Do(_ => 
            {
                attemptCount++;
                int randomNumber = random.Next(0, 11);
                // 确保每次抽取都立即输出
                LogBox1.AppendText($"第{attemptCount}次尝试: 抽到数字 {randomNumber}");
                if (randomNumber == 5)
                {
                    hasSuccess = true;
                    LogBox1.AppendText(" - 成功！" + Environment.NewLine);
                    LogBox1.AppendText($"恭喜！在第{attemptCount}次成功抽到数字5" + Environment.NewLine);
                }
                else
                {
                   LogBox1.AppendText(" - 失败" + Environment.NewLine);
                }
            })
            .Subscribe(
                _ => { },
                ex => LogBox1.AppendText($"最终结果: {ex.Message}" + Environment.NewLine),
                () => 
                {
                    if (!hasSuccess)
                    {
                        LogBox1.AppendText($"已达到最大尝试次数{maxAttempts}，未能抽到数字5" + Environment.NewLine);
                    }
                    LogBox1.AppendText("抽奖过程结束" + Environment.NewLine);
                }
            );
    }
}