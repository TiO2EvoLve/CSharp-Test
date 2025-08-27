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

        Observable.Interval(TimeSpan.FromSeconds(1))
            .Select(i =>
            {
                if (i == 3) throw new Exception("模拟错误：i == 3");
                return i;
            })
            .Retry(2) // 出错后最多重试 2 次（即总共最多执行 3 次）
            .ObserveOn(SynchronizationContext.Current!)
            .Subscribe(
                value => LogBox1.AppendText($"收到: {value}{Environment.NewLine}"),
                ex => LogBox1.AppendText($"最终错误: {ex.Message}{Environment.NewLine}"),
                () => LogBox1.AppendText("完成{Environment.NewLine}")
            );
    }
}