namespace Test.Design.Chain_of_Responsibility;

// 定义处理者接口
public interface IHandler
{
    // 设置下一个处理者
    IHandler SetNext(IHandler handler);
    // 处理请求
    object Handle(object request);
}