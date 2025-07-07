namespace Test.ViewModels;

public class SoundConfig
{
    public bool UI { get; set; }
    public string Volume { get; set; }
    public string Pitch { get; set; }
    public bool Loop { get; set; }
    public string Mode { get; set; }
    public List<string> Sounds { get; set; } = new();
}