namespace Test.Design.观察者模式;

public class PhoneDisplay(string name)
{
    public void Subscribe(WeatherStation station)
    {
        station.OnTemperatureChanged += Update;
    }

    public void Unsubscribe(WeatherStation station)
    {
        station.OnTemperatureChanged -= Update;
    }

    private void Update(float temperature)
    {
        Console.WriteLine($"[{name}] 收到天气更新：当前温度为 {temperature}°C");
    }
}