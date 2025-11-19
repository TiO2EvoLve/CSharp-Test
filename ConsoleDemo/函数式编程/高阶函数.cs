namespace Test.ConsoleDemo.函数式编程;

public class 高阶函数
{
    // 函数作为参数
    public static int Calculate(int x, int y, Func<int, int, int> operation)
    {
        return operation(x, y);
    }

    // 函数作为返回值
    public static Func<int, int> Multiplier(int factor)
    {
        return x => x * factor;
    }

    // 使用示例
    public static void Run()
    {
        var result = Calculate(5, 3, (a, b) => a + b);
        var doubleIt = Multiplier(2);
        Console.WriteLine(doubleIt(5)); // 输出 10
    }
}