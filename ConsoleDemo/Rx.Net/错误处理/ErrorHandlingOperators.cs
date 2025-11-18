using System.Reactive.Linq;

namespace Test.ConsoleDemo.Rx.Net.错误处理;

public class ErrorHandlingOperators
{
    public static void Demo()
    {
        Console.WriteLine("\n=== 错误处理操作符 ===");

        // 1. Catch - 捕获错误并继续
        var faultyObservable = Observable.Throw<int>(new Exception("Something went wrong!"));
        var fallbackObservable = Observable.Return(999);

        faultyObservable
            .Catch<int, Exception>(ex =>
            {
                Console.WriteLine($"Catch - Error caught: {ex.Message}");
                return fallbackObservable;
            })
            .Subscribe(
                value => Console.WriteLine($"Catch - Value: {value}"),
                error => Console.WriteLine($"Catch - Should not reach here: {error.Message}")
            );

        // 2. Retry - 重试
        var attempt = 0;
        var retryObservable = Observable.Create<int>(observer =>
        {
            attempt++;
            Console.WriteLine($"Retry - Attempt: {attempt}");
            if (attempt < 3)
            {
                observer.OnError(new Exception($"Attempt {attempt} failed"));
            }
            else
            {
                observer.OnNext(42);
                observer.OnCompleted();
            }

            return () => { };
        });

        retryObservable
            .Retry(3) // 最多重试3次
            .Subscribe(
                value => Console.WriteLine($"Retry - Success: {value}"),
                error => Console.WriteLine($"Retry - Final error: {error.Message}")
            );

        // 3. Finally - 最终清理
        Observable.Range(1, 3)
            .Finally(() => Console.WriteLine("Finally - Cleanup completed"))
            .Subscribe(
                value => Console.WriteLine($"Finally - Value: {value}"),
                () => Console.WriteLine("Finally - Sequence completed")
            );
    }
}