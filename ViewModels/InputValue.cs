
public class InputValue : BindableBase
{
    private string _input;
    public string Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }
}