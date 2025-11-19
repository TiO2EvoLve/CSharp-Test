namespace Test.ConsoleDemo.函数式编程;

public class 函数组合
{
    public static void Run()
    {
        Func<int, int> add2 = x => x + 2;
        Func<int, int> mul10 = x => x * 10;

        Func<int, int> composed = x => mul10(add2(x));

        Console.WriteLine(composed(3)); // (3+2)*10 = 50
    }
}