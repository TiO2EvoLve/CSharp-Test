using System.Reflection;
using System.Windows;

namespace Test;

public partial class 反射测试 : Window
{
    public 反射测试()
    {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        // 1. 获取类型信息
        Type personType = typeof(Person);
        Console.WriteLine($"1. 获取Person类型信息: {personType.FullName}");
            
        // 2. 创建对象实例
        object personInstance = Activator.CreateInstance(personType);
        Console.WriteLine("\n2. 创建Person实例: " + personInstance);
            
        // 3. 设置属性值
        PropertyInfo nameProperty = personType.GetProperty("Name");
        nameProperty.SetValue(personInstance, "张三");
        Console.WriteLine("\n3. 设置Name属性为'张三'");
            
        // 4. 获取属性值
        string nameValue = (string)nameProperty.GetValue(personInstance);
        Console.WriteLine($"4. 获取Name属性值: {nameValue}");
            
        // 5. 调用方法
        MethodInfo greetMethod = personType.GetMethod("Greet");
        greetMethod.Invoke(personInstance, null);
            
        // 6. 调用带参数的方法
        MethodInfo introduceMethod = personType.GetMethod("Introduce");
        introduceMethod.Invoke(personInstance, new object[] { "李四" });
            
        // 7. 访问私有成员(通常不推荐这样做)
        FieldInfo ageField = personType.GetField("age", BindingFlags.NonPublic | BindingFlags.Instance);
        ageField.SetValue(personInstance, 30);
        Console.WriteLine($"\n7. 通过反射设置私有字段age为30");
            
        // 8. 获取程序集信息
        Assembly assembly = Assembly.GetExecutingAssembly();
        Console.WriteLine("\n8. 当前程序集信息:");
        Console.WriteLine($"程序集名称: {assembly.FullName}");
        Console.WriteLine("程序集中定义的类型:");
        foreach (Type type in assembly.GetTypes())
        {
            Console.WriteLine($"- {type.Name}");
        }
    }
}
public class Person
{
    private int age;
        
    public string Name { get; set; }
        
    public void Greet()
    {
        Console.WriteLine($"\n5. {Name}说: 你好!");
    }
        
    public void Introduce(string to)
    {
        Console.WriteLine($"\n6. {Name}对{to}说: 我今年{age}岁");
    }
}