namespace Test.Design.建造者模式;

// 电脑类，包含电脑的各个组件
public class Computer
{
    public string CPU { get; set; }
    public string Memory { get; set; }
    public string HardDrive { get; set; }

    // 重写 ToString 方法，方便输出电脑信息
    public override string ToString()
    {
        return $"CPU: {CPU}, Memory: {Memory}, Hard Drive: {HardDrive}";
    }
}