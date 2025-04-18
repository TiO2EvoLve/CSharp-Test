using System.Windows;
using Test.Design;
using Test.Design.Factory;
using Test.Design.建造者模式;

namespace Test;

public partial class DesignTest : Window
{
    public DesignTest()
    {
        InitializeComponent();
    }

    private void 单例模式(object sender, RoutedEventArgs e)
    {
        Singleton singleton1 = Singleton.Instance;
        Singleton singleton2 = Singleton.Instance;
        Console.WriteLine(singleton1 == singleton2);
    }

    private void 责任链模式(object sender, RoutedEventArgs e)
    {
        // 创建具体处理者实例
        ConcreteHandlerA handlerA = new ConcreteHandlerA();
        ConcreteHandlerB handlerB = new ConcreteHandlerB();
        ConcreteHandlerC handlerC = new ConcreteHandlerC();

        // 构建责任链
        handlerA.SetNext(handlerB).SetNext(handlerC);

        // 定义请求
        object[] requests = { "A request", "B request", "C request", "D request" };

        // 遍历请求并传递给责任链处理
        foreach (object request in requests)
        {
            Console.WriteLine($"Client: Who wants to handle {request}?");
            object result = handlerA.Handle(request);
            if (result != null)
            {
                Console.WriteLine($"  {result}");
            }
            else
            {
                Console.WriteLine($"  {request} was left unhandled.");
            }
        }
    }

    private void 简单工厂模式(object sender, RoutedEventArgs e)
    {
        IProduct productA = SimpleFactory.CreateProduct("A");
        productA.Operation();

        IProduct productB = SimpleFactory.CreateProduct("B");
        productB.Operation();
    }

    private void 建造者模式(object sender, RoutedEventArgs e)
    {
        // 创建高端配置电脑的建造者
        ComputerBuilder highEndBuilder = new HighEndComputerBuilder();
        // 创建指挥者，并传入高端配置电脑的建造者
        ComputerDirector highEndDirector = new ComputerDirector(highEndBuilder);
        // 指挥者指挥建造者构建高端配置电脑
        Computer highEndComputer = highEndDirector.Construct();
        Console.WriteLine("High End Computer: " + highEndComputer);

        // 创建低端配置电脑的建造者
        ComputerBuilder lowEndBuilder = new LowEndComputerBuilder();
        // 创建指挥者，并传入低端配置电脑的建造者
        ComputerDirector lowEndDirector = new ComputerDirector(lowEndBuilder);
        // 指挥者指挥建造者构建低端配置电脑
        Computer lowEndComputer = lowEndDirector.Construct();
        Console.WriteLine("Low End Computer: " + lowEndComputer);
    }
}