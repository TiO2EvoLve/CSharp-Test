namespace Test.Design.建造者模式;

// 指挥者类，负责指挥建造者构建电脑
public class ComputerDirector
{
    private ComputerBuilder builder;

    public ComputerDirector(ComputerBuilder builder)
    {
        this.builder = builder;
    }

    // 构建电脑的方法
    public Computer Construct()
    {
        builder.BuildCPU();
        builder.BuildMemory();
        builder.BuildHardDrive();
        return builder.GetComputer();
    }
}