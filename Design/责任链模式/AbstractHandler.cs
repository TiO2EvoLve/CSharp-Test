namespace Test.Design.Chain_of_Responsibility;

// 抽象处理者类
public abstract class AbstractHandler : IHandler
{
    // 下一个处理者
    private IHandler nextHandler;

    public IHandler SetNext(IHandler handler)
    {
        this.nextHandler = handler;
        // 返回下一个处理者，方便链式调用
        return handler;
    }

    public virtual object Handle(object request)
    {
        if (this.nextHandler != null)
        {
            // 如果有下一个处理者，将请求传递给下一个处理者
            return this.nextHandler.Handle(request);
        }
        else
        {
            // 没有下一个处理者，返回null
            return null;
        }
    }
}