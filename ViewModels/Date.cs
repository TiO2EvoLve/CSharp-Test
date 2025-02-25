namespace Test.ViewModels;

public class Date:BindableBase
{
    private string _datetime;
    public string Datetime
    {
        get => _datetime;
        set => SetProperty(ref _datetime, value);
    }

    public void DateTestViewModel()
    {
        Datetime = DateTime.Now.ToString("F");
    }
}