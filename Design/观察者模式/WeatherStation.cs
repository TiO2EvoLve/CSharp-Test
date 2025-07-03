namespace Test.Design.观察者模式;

public class WeatherStation
{
    public event Action<float> OnTemperatureChanged;

    private float temperature;

    public void SetTemperature(float temp)
    {
        Console.WriteLine($"\n[WeatherStation] 温度更新为：{temp}°C");
        temperature = temp;
        OnTemperatureChanged?.Invoke(temperature); // 通知所有订阅者
    }
}