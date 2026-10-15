using System.Reflection;
using CSharpConsoleApp.Examples;

namespace CSharpConsoleApp.Features;

internal static class TopicTour
{
    public static async Task RunAsync()
    {
        Console.WriteLine("\nC# topic tour\n");

        // Syntax, variables, types, nullable values, operators, control flow, and methods.
        int age = 20;
        double score = 92.5;
        string name = "Alex";
        string? nickname = null;
        int? optionalNumber = null;
        bool isAdult = age >= 18 && score > 80;
        Console.WriteLine($"Variables and types: {DescribePerson(name, age)}, score {score}");
        Console.WriteLine($"Nullable values: nickname = {nickname ?? "none"}, number = {optionalNumber?.ToString() ?? "none"}");
        Console.WriteLine($"Operators and control flow: {(isAdult ? "adult" : "not an adult")}");

        // Classes, objects, properties, constructors, OOP, and interfaces.
        var account = new BankAccount("Alex", 100);
        account.Deposit(25);
        Console.WriteLine($"Class, object, property, constructor: {account.Describe()}");

        // Abstract classes, inheritance, and polymorphism.
        Shape[] shapes = [new Circle(2), new Rectangle(3, 4)];
        foreach (var shape in shapes)
        {
            Console.WriteLine($"Polymorphism: {shape.GetType().Name} area = {shape.Area():0.##}");
        }

        // Generics and collections.
        var favorites = new List<string> { "C#", "LINQ", "classes" };
        var box = new Box<string>(favorites[0]);
        Console.WriteLine($"Generic Box<T>: {box.Value}; collection count = {favorites.Count}");

        // Exceptions.
        try
        {
            _ = int.Parse("not a number");
        }
        catch (FormatException exception)
        {
            Console.WriteLine($"Exception handling: {exception.GetType().Name} was handled.");
        }

        // Delegates, lambdas, and LINQ.
        Func<int, bool> isEven = number => number % 2 == 0;
        var evenNumbers = Enumerable.Range(1, 6).Where(isEven).ToList();
        Console.WriteLine($"Delegate/lambda and LINQ: {string.Join(", ", evenNumbers)}");

        // Async/await, Task, and CancellationToken.
        using var cancellation = new CancellationTokenSource();
        Task<string> messageTask = GetMessageAsync(cancellation.Token);
        Console.WriteLine($"Async Task: {await messageTask}");

        // Records, structs, and pattern matching.
        var person = new PersonRecord("Sam", 30);
        var point = new Point(3, 4);
        var shapeDescription = shapes[0] switch
        {
            Circle circle => $"circle with radius {circle.Radius}",
            Rectangle rectangle => $"rectangle {rectangle.Width} by {rectangle.Height}",
            _ => "unknown shape"
        };
        Console.WriteLine($"Record: {person}; struct: {point}; pattern matching: {shapeDescription}");

        // Extension methods, attributes, reflection, memory management, GC, and IDisposable.
        Console.WriteLine($"Extension method: {"csharp".ToTopicLabel()}");
        var info = typeof(DemoFeature).GetCustomAttribute<DemoInfoAttribute>();
        Console.WriteLine($"Attribute and reflection: {info?.Description}");
        using (var resource = new DemoResource())
        {
            Console.WriteLine($"Memory management: GC manages memory; current managed heap = {GC.GetTotalMemory(false)} bytes.");
        }
        Console.WriteLine("IDisposable: resource was disposed when its using block ended.");

        Console.WriteLine("\nNullable reference types are enabled in the project; string? marks a value that may be null.");
    }

    private static string DescribePerson(string name, int age) => $"{name}, age {age}";

    private static async Task<string> GetMessageAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(50, cancellationToken);
        return "completed with a cancellation token";
    }
}
