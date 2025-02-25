namespace Test.Design.建造者模式;

// 抽象建造者类，定义了构建电脑各个组件的抽象方法
public abstract class ComputerBuilder
{
    protected Computer computer = new Computer();

    public abstract void BuildCPU();
    public abstract void BuildMemory();
    public abstract void BuildHardDrive();

    // 获取最终构建好的电脑对象
    public Computer GetComputer()
    {
        return computer;
    }
}