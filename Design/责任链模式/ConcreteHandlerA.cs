namespace Test.Design.Chain_of_Responsibility;

// 具体处理者类A
public class ConcreteHandlerA : AbstractHandler
{
    public override object Handle(object request)
    {
        if ((request as string)?.StartsWith("A") == true)
        {
            // 如果请求以A开头，处理该请求
            return $"ConcreteHandlerA: Handled request {request}";
        }
        else
        {
            // 否则，将请求传递给下一个处理者
            return base.Handle(request);
        }
    }
}

// 具体处理者类B
public class ConcreteHandlerB : AbstractHandler
{
    public override object Handle(object request)
    {
        if ((request as string)?.StartsWith("B") == true)
        {
            // 如果请求以B开头，处理该请求
            return $"ConcreteHandlerB: Handled request {request}";
        }
        else
        {
            // 否则，将请求传递给下一个处理者
            return base.Handle(request);
        }
    }
}

// 具体处理者类C
public class ConcreteHandlerC : AbstractHandler
{
    public override object Handle(object request)
    {
        if ((request as string)?.StartsWith("C") == true)
        {
            // 如果请求以C开头，处理该请求
            return $"ConcreteHandlerC: Handled request {request}";
        }
        else
        {
            // 否则，将请求传递给下一个处理者
            return base.Handle(request);
        }
    }
}