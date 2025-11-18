namespace Test.ViewModels;

public class User
{
    public User(string name, int age, string sex)
    {
        this.name = name;
        this.age = age;
        this.sex = sex;
    }

    public User()
    {
    }

    public int id { get; set; }
    public string name { get; set; }
    public int age { get; set; }

    public string sex { get; set; }
}