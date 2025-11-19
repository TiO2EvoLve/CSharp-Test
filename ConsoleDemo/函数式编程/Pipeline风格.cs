namespace Test.ConsoleDemo.函数式编程;

public class Pipeline风格
{
    public static void Run()
    {
        var result = 5.Pipe(x => x + 1)
                .Pipe(x => x * 10);

        Console.WriteLine(result); // 60
    }
}
public static class PipeExtensions
{
    public static TResult Pipe<TSource, TResult>(this TSource value, Func<TSource, TResult> func)
        => func(value);
}