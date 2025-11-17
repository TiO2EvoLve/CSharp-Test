using System.Windows;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace Test;

public partial class 依赖注入测试
{
    public 依赖注入测试()
    {
        InitializeComponent();
    }

    // 定义服务接口
    public interface IMessageService
    {
        void Send(string msg);
    }

    // 实现接口：邮件服务
    public class EmailService : IMessageService
    {
        public void Send(string msg) => Console.WriteLine($"Email: {msg}");
    }

    private void ContainerTest(object sender, RoutedEventArgs e)
    {
        var builder = new ContainerBuilder();

        // 注册服务
        builder.RegisterType<EmailService>().As<IMessageService>().SingleInstance();

        var container = builder.Build();

        // 解析
        var services = container.Resolve<IMessageService>();

        services.Send("Hello Autofac!");
    }
}