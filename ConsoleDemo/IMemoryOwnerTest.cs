using System.Buffers;

namespace Test.ConsoleDemo;

//内存管理IMemoryOwner接口测试
public class IMemoryOwnerTest
{
    public static void Run()
    {
        Task.Run(async () => await ProcessDataAsync()).GetAwaiter().GetResult();
    }

    private static async Task ProcessDataAsync()
    {
        using (IMemoryOwner<byte> buffer = MemoryPool<byte>.Shared.Rent(1024))
        {
            // 使用内存缓冲区
            Memory<byte> memory = buffer.Memory;

            // 模拟异步数据处理
            await ProcessMemoryAsync(memory.Slice(0, 100));

            Console.WriteLine("缓冲区处理完成");
        } // 自动释放缓冲区回内存池
    }

    private static async Task ProcessMemoryAsync(Memory<byte> memory)
    {
        // 处理内存数据
        await Task.Delay(100);
    }
}