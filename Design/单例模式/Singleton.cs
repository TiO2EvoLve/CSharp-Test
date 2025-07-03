namespace Test.Design;

public class Singleton
{
    //懒汉式（非线程安全）
    //这种实现方式在第一次使用时才创建实例，但在多线程环境下可能会创建多个实例，因此不是线程安全的。
    // 私有静态字段，用于存储单例实例
    private static Singleton instance;
    // 私有构造函数，防止外部实例化
    private Singleton() { }
    // 公共静态属性，用于获取单例实例
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
    
    //静态初始化（线程安全）
    //这种方式利用了 C# 静态构造函数的特性，在类被加载时就创建实例，是线程安全的。
    // 私有静态字段，用于存储单例实例
    private static readonly Singleton _instance = new Singleton();

    // 私有构造函数，防止外部实例化
    //private Singleton() { }

    // 公共静态属性，用于获取单例实例
    public static Singleton _Instance
    {
        get
        {
            return _instance;
        }
    }
}