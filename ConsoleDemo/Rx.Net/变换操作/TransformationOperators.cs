using System.Reactive.Linq;

namespace Test.ConsoleDemo.Rx.Net.变换操作;

public class TransformationOperators
{
    public static void Demo()
    {
        Console.WriteLine("\n=== 变换操作符 ===");

        // 1. Select (Map) - 投影转换
        Observable.Range(1, 5)
            .Select(x => x * x)
            .Subscribe(value => Console.WriteLine($"Select: {value}"));

        // 2. SelectMany - 扁平化转换
        Observable.Range(1, 3)
            .SelectMany(x => Observable.Range(1, x))
            .Subscribe(value => Console.WriteLine($"SelectMany: {value}"));

        // 3. Buffer - 缓冲多个值
        Observable.Interval(TimeSpan.FromMilliseconds(500))
            .Take(10)
            .Buffer(3) // 每3个值缓冲一次
            .Subscribe(buffer =>
                Console.WriteLine($"Buffer: [{string.Join(", ", buffer)}]"));

        // 4. Window - 窗口化（类似Buffer但返回Observable）
        Observable.Interval(TimeSpan.FromMilliseconds(400))
            .Take(8)
            .Window(3)
            .Subscribe(window =>
            {
                Console.Write("Window: ");
                window.Subscribe(value => Console.Write($"{value} "));
                Console.WriteLine();
            });

        // 5. GroupBy - 分组
        var words = new[] { "apple", "banana", "cat", "dog", "elephant" };
        words.ToObservable()
            .GroupBy(word => word.Length)
            .Subscribe(group =>
            {
                Console.Write($"GroupBy (Length {group.Key}): ");
                group.Subscribe(word => Console.Write($"{word} "));
                Console.WriteLine();
            });

        Task.Delay(3000).Wait();
    }
}