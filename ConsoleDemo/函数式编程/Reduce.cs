namespace Test.ConsoleDemo.函数式编程;

public class Reduce
{
    public static void Run()
    {
        var sum = new[] { 1, 2, 3, 4 }.Aggregate((a, b) => a + b);
        Console.WriteLine(sum); // 10
    }
}