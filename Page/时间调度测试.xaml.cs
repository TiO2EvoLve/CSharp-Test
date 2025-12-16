using System.Windows;
using Quartz;
using Quartz.Impl;
using Window = System.Windows.Window;

namespace Test.Page;

public partial class 时间调度测试 : Window
{
    public 时间调度测试()
    {
        InitializeComponent();
    }

    private void StartTime(object sender, RoutedEventArgs e)
    {
        Run().GetAwaiter().GetResult();
    }

    private async Task Run()
    {
        Console.WriteLine("创建定时任务调度器...");
        // 2. 创建调度器工厂并获取调度器
        ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
        var scheduler = await schedulerFactory.GetScheduler();

        // 启动调度器
        await scheduler.Start();
        Console.WriteLine("调度器启动成功！");

        // 3. 创建一个作业（Job）
        var job = JobBuilder.Create<HelloWorldJob>()
            .WithIdentity("helloWorldJob", "group1") // 设置作业的唯一标识和组名
            .Build();

        // 4. 创建一个触发器（Trigger）
        var trigger = TriggerBuilder.Create()
            .WithIdentity("noonTrigger", "group1") // 设置触发器的唯一标识和组名
            .WithCronSchedule("0 0/1 * * * ?") //每分钟
            .Build();

        // 5. 将作业和触发器添加到调度器中
        await scheduler.ScheduleJob(job, trigger);
        Console.WriteLine("已安排任务");

        // // 6. 关闭调度器
        // await scheduler.Shutdown();
        // Console.WriteLine("调度器已关闭。");
    }
}

// 1. 定义要执行的任务
public class HelloWorldJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        // 到达时间后执行的任务逻辑
        Console.WriteLine($"Hello World! 当前时间: {DateTime.Now}");
        return Task.CompletedTask;
    }
}