namespace Test.ConsoleDemo.函数式编程;

public class Monads风格
{
    public static void Run()
    {
        Maybe<int> ParseInt(string s)
        {
            return int.TryParse(s, out var v) ? new Maybe<int>(v) : new Maybe<int>();
        }

        var result =
            ParseInt("10")
                .Bind(x => new Maybe<int>(x * 2));

        Console.WriteLine(result.HasValue ? result.Value : 0); // 20
    }
}
public struct Maybe<T>
{
    public T Value { get; }
    public bool HasValue { get; }

    public Maybe(T value) { Value = value; HasValue = true; }

    public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> func)
        => HasValue ? func(Value) : new Maybe<TResult>();
}