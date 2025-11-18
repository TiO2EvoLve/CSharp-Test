using System.Reactive.Linq;

namespace Test.ConsoleDemo.Rx.Net.组合操作;

public class CombinationOperators
{
    public static void Demo()
    {
        Console.WriteLine("\n=== 组合操作符 ===");

        var obs1 = Observable.Range(1, 3);
        var obs2 = Observable.Range(10, 3);
        var obs3 = Observable.Range(100, 3);

        // 1. Merge - 合并多个Observable
        obs1.Merge(obs2)
            .Subscribe(value => Console.WriteLine($"Merge: {value}"));

        // 2. Concat - 顺序连接
        obs1.Concat(obs2)
            .Subscribe(value => Console.WriteLine($"Concat: {value}"));

        // 3. Zip - 一对一组合
        obs1.Zip(obs2, (a, b) => $"{a}+{b}={a + b}")
            .Subscribe(value => Console.WriteLine($"Zip: {value}"));

        // 4. CombineLatest - 任意一个发射时组合最新值
        var fast = Observable.Interval(TimeSpan.FromMilliseconds(400)).Take(3);
        var slow = Observable.Interval(TimeSpan.FromMilliseconds(800)).Take(3);

        fast.CombineLatest(slow, (f, s) => $"Fast:{f}, Slow:{s}")
            .Subscribe(value => Console.WriteLine($"CombineLatest: {value}"));

        // 5. Switch - 切换到最新的Observable
        var outer = Observable.Interval(TimeSpan.FromSeconds(1))
            .Take(2)
            .Select(i => Observable.Interval(TimeSpan.FromMilliseconds(400))
                .Take(3)
                .Select(j => $"{i}-{j}"));

        outer.Switch()
            .Subscribe(value => Console.WriteLine($"Switch: {value}"));

        Task.Delay(3000).Wait();
    }
}