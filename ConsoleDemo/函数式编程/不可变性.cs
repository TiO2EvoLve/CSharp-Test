namespace Test.ConsoleDemo.函数式编程;

public class 不可变性
{
    public static void Run()
    {
        var p1 = new Person("Jack", 20);
        // 使用 with 创建修改后的拷贝，不改变原对象
        var p2 = p1 with { Age = 21 };

        Console.WriteLine(p1); // Person { Name = Jack, Age = 20 }
        Console.WriteLine(p2); // Person { Name = Jack, Age = 21 }
    }

    // 使用 record 类型（C# 9+）
    private record Person(string Name, int Age);
}