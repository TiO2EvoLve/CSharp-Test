using System.Windows;
using System.Windows.Media;

namespace Test;

public partial class SyncTest
{
    public SyncTest()
    {
        InitializeComponent();
    }
    private async void Button_OnClick(object sender, RoutedEventArgs e)
    {
        OnReset();
        List<Action> actions =
        [
            () =>
            {
                Console.WriteLine("任务一开始");
                Task.Delay(1000).Wait();
                Console.WriteLine("任务一完成");
                Dispatcher.Invoke(() =>
                {
                    任务1.Text = "任务一完成";
                    Label1.Background = Brushes.GreenYellow;
                });
            },
            () =>
            {
                Console.WriteLine("任务二开始");
                Task.Delay(2000).Wait();
                Console.WriteLine("任务二完成");
                Dispatcher.Invoke(() =>
                {
                    任务2.Text = "任务二完成";
                    Label2.Background = Brushes.GreenYellow;
                });
            },
            () =>
            {
                Console.WriteLine("任务三开始");
                Task.Delay(3000).Wait();
                Console.WriteLine("任务三完成");
                Dispatcher.Invoke(() =>
                {
                    任务3.Text = "任务三完成";
                    Label3.Background = Brushes.GreenYellow;
                });

            }
        ];
        // 将所有 Action 包装为 Task 并等待完成
        List<Task> tasks = new List<Task>();
        foreach (var action in actions)
        { 
            tasks.Add(Task.Run(action));
        }
        await Task.WhenAny(tasks);
        Console.WriteLine("至少有一个完成");
        await Task.WhenAll(tasks);
        Console.WriteLine("已全部完成");
        text.Text = "已全部完成";
        OnFinish();
    }
    private void OnReset()
    {
        text.Text = "开始任务";
        任务1.Text = "任务一开始";
        任务2.Text = "任务二开始";
        任务3.Text = "任务三开始";
        Label1.Background = Brushes.White;
        Label2.Background = Brushes.White;
        Label3.Background = Brushes.White;
        Label1.BorderBrush = Brushes.Gainsboro;
        Label2.BorderBrush = Brushes.Gainsboro;
        Label3.BorderBrush = Brushes.Gainsboro;
        Label1.BorderThickness = new Thickness(1);
        Label2.BorderThickness = new Thickness(1);
        Label3.BorderThickness = new Thickness(1); 
    }
    private void OnFinish()
    {
        Label1.BorderBrush = Brushes.Gold;
        Label2.BorderBrush = Brushes.Gold;
        Label3.BorderBrush = Brushes.Gold;
        Label1.BorderThickness = new Thickness(3);
        Label2.BorderThickness = new Thickness(3);
        Label3.BorderThickness = new Thickness(3);
    }
}