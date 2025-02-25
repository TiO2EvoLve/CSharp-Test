namespace Test.Design.Factory;

// 具体产品类A
public class ConcreteProductA : IProduct
{
    public void Operation()
    {
        Console.WriteLine("ConcreteProductA operation");
    }
}

// 具体产品类B
public class ConcreteProductB : IProduct
{
    public void Operation()
    {
        Console.WriteLine("ConcreteProductB operation");
    }
}

// 简单工厂类
public class SimpleFactory
{
    public static IProduct CreateProduct(string type)
    {
        switch (type)
        {
            case "A":
                return new ConcreteProductA();
            case "B":
                return new ConcreteProductB();
            default:
                throw new ArgumentException("Invalid product type");
        }
    }
}