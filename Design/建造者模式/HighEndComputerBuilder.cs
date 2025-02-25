namespace Test.Design.建造者模式;

// 具体建造者类，用于构建高端配置电脑
public class HighEndComputerBuilder : ComputerBuilder
{
    public override void BuildCPU()
    {
        computer.CPU = "Intel Core i9";
    }

    public override void BuildMemory()
    {
        computer.Memory = "32GB DDR4";
    }

    public override void BuildHardDrive()
    {
        computer.HardDrive = "1TB SSD";
    }
}

// 具体建造者类，用于构建低端配置电脑
public class LowEndComputerBuilder : ComputerBuilder
{
    public override void BuildCPU()
    {
        computer.CPU = "Intel Core i3";
    }

    public override void BuildMemory()
    {
        computer.Memory = "8GB DDR4";
    }

    public override void BuildHardDrive()
    {
        computer.HardDrive = "500GB HDD";
    }
}