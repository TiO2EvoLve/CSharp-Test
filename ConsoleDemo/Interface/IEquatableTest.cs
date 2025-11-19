namespace Test.ConsoleDemo.Interface;

//可等同性IEquatable接口测试
public class IEquatableTest
{
    public static void Run()
    {
        // 使用示例
        var person1 = new Person { Name = "John", Age = 25 };
        var person2 = new Person { Name = "John", Age = 25 };

        Console.WriteLine(person1.Equals(person2)); // true - 调用IEquatable<T>.Equals
        Console.WriteLine(person1.Equals((object)person2)); // true - 调用object.Equals
    } 
}
public class Person : IEquatable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    // 实现IEquatable<T>
    public bool Equals(Person other)
    {
        if (other is null) return false;
        return Name == other.Name && Age == other.Age;
    }
    
    // 重写Object.Equals保持一致性
    public override bool Equals(object obj) => Equals(obj as Person);
    
    // 重写GetHashCode很重要！
    public override int GetHashCode() => HashCode.Combine(Name, Age);
}