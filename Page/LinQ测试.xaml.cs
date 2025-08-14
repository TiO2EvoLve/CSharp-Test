using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Test;

public partial class LinQ测试
{
    public LinQ测试()
    {
        InitializeComponent();
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    private void 筛选(object sender, RoutedEventArgs e)
    {
        var people = new List<Person>
        {
            new () { Name = "Alice", Age = 17 },
            new () { Name = "Bob", Age = 20 },
            new () { Name = "Charlie", Age = 15 },
            new () { Name = "Daisy", Age = 22 }
        };

        var adults = people.Where(p => p.Age > 18);

        foreach (var person in adults)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
    }
    private void 操作(object sender, RoutedEventArgs e)
    {
        var numbers = new[] { 1, 2, 3, 4, 5 };
        var squares = numbers.Select(n => n * n);

        foreach (var square in squares)
        {
            Console.WriteLine(square);
        }
    }
    private void 排序(object sender, RoutedEventArgs e)
    {
        var people = new[]
        {
            new { Name = "Alice", Age = 25 },
            new { Name = "Bob", Age = 20 },
            new { Name = "Charlie", Age = 30 }
        };

        var sortedByAge = people.OrderBy(p => p.Age);
        var sortedByAgeDesc = people.OrderByDescending(p => p.Age);

        Console.WriteLine("Ascending:");
        foreach (var person in sortedByAge)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }

        Console.WriteLine("\nDescending:");
        foreach (var person in sortedByAgeDesc)
        {
            Console.WriteLine($"{person.Name}, {person.Age}");
        }
    }
    private void 分组(object sender, RoutedEventArgs e)
    {
        var people = new[]
        {
            new { Name = "Alice", Age = 25 },
            new { Name = "Bob", Age = 20 },
            new { Name = "Charlie", Age = 25 },
            new { Name = "Daisy", Age = 20 }
        };

        var groupedByAge = people.GroupBy(p => p.Age);

        foreach (var group in groupedByAge)
        {
            Console.WriteLine($"Age: {group.Key}");
            foreach (var person in group)
            {
                Console.WriteLine($"{person.Name}");
            }
        }
    } 
    private void 连接(object sender, RoutedEventArgs e)
    {
        var students = new[]
        {
            new { Id = 1, Name = "Alice" },
            new { Id = 2, Name = "Bob" },
            new { Id = 3, Name = "Charlie" }
        };

        var grades = new[]
        {
            new { StudentId = 1, Grade = "A" },
            new { StudentId = 2, Grade = "B" },
            new { StudentId = 3, Grade = "A" }
        };

        var studentGrades = students.Join(
            grades,
            student => student.Id,
            grade => grade.StudentId,
            (student, grade) => new { student.Name, grade.Grade }
        );

        foreach (var sg in studentGrades)
        {
            Console.WriteLine($"{sg.Name}: {sg.Grade}");
        }
    }
    private void 统计(object sender, RoutedEventArgs e)
    {
        var numbers = new[] { 1, 2, 3, 4, 5 };

        Console.WriteLine($"Count: {numbers.Count()}");
        Console.WriteLine($"Sum: {numbers.Sum()}");
        Console.WriteLine($"Average: {numbers.Average()}");
        Console.WriteLine($"Min: {numbers.Min()}");
        Console.WriteLine($"Max: {numbers.Max()}");
    }
    private void 查找(object sender, RoutedEventArgs e)
    {
        var numbers = new[] { 1, 3, 5, 8, 10 };

        var firstEven = numbers.FirstOrDefault(n => n % 2 == 0);
        var lastEven = numbers.LastOrDefault(n => n % 2 == 0);
        
        Console.WriteLine($"{firstEven}-{lastEven}"); 
    }
    private void 转换(object sender, RoutedEventArgs e)
    {
        var people = new[]
        {
            new { Id = 1, Name = "Alice" },
            new { Id = 2, Name = "Bob" }
        };
        var dictionary = people.ToDictionary(p => p.Id, p => p.Name);

        foreach (var kvp in dictionary)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
    
}