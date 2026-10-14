using System.Reflection;

Console.WriteLine("C# Learning Console");
Console.WriteLine("1. Explore C# topics");
Console.WriteLine("2. Use the Q&A bot");
Console.Write("Choose an option: ");

if (Console.ReadLine() == "2")
{
    RunQuestionBot();
}
else
{
    await RunTopicTourAsync();
}

static void RunQuestionBot()
{
    var answers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        ["hello"] = "Hello! How can I help?",
        ["hi"] = "Hi there!",
        ["how are you"] = "I'm doing well, thanks for asking!",
        ["what is your name"] = "I'm a simple question-and-answer bot.",
        ["who made you"] = "I was created as a simple C# console app.",
        ["what can you do"] = "I can answer a few predefined questions. Type 'help' to see examples.",
        ["what is c sharp"] = "C# is a modern programming language developed by Microsoft."
    };

    Console.WriteLine("Ask a question, type 'help' for examples, or type 'exit' to quit.");

    while (true)
    {
        Console.Write("\nYou: ");
        var input = Console.ReadLine();

        if (input is null)
        {
            break;
        }

        var question = string.Join(' ', input.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries))
            .ToLowerInvariant();

        if (question is "exit" or "quit")
        {
            Console.WriteLine("Bot: Goodbye!");
            break;
        }

        if (question == "help")
        {
            Console.WriteLine("Bot: Try asking:");
            foreach (var predefinedQuestion in answers.Keys)
            {
                Console.WriteLine($"  - {predefinedQuestion}?");
            }

            continue;
        }

        if (answers.TryGetValue(question, out var answer))
        {
            Console.WriteLine($"Bot: {answer}");
        }
        else
        {
            Console.WriteLine("Bot: I don't know that one yet. Type 'help' to see what you can ask.");
        }
    }
}

static async Task RunTopicTourAsync()
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

static string DescribePerson(string name, int age) => $"{name}, age {age}";

static async Task<string> GetMessageAsync(CancellationToken cancellationToken)
{
    await Task.Delay(50, cancellationToken);
    return "completed with a cancellation token";
}

interface IDescribable
{
    string Describe();
}

sealed class BankAccount : IDescribable
{
    public string Owner { get; }
    public decimal Balance { get; private set; }

    public BankAccount(string owner, decimal openingBalance)
    {
        Owner = owner;
        Balance = openingBalance;
    }

    public void Deposit(decimal amount) => Balance += amount;

    public string Describe() => $"{Owner}'s account balance is {Balance:C}";
}

abstract class Shape
{
    public abstract double Area();
}

sealed class Circle(double radius) : Shape
{
    public double Radius { get; } = radius;

    public override double Area() => Math.PI * Radius * Radius;
}

sealed class Rectangle(double width, double height) : Shape
{
    public double Width { get; } = width;
    public double Height { get; } = height;

    public override double Area() => Width * Height;
}

sealed class Box<T>(T value)
{
    public T Value { get; } = value;
}

record PersonRecord(string Name, int Age);

readonly record struct Point(int X, int Y);

static class StringExtensions
{
    public static string ToTopicLabel(this string value) => $"[{value}]";
}

[AttributeUsage(AttributeTargets.Class)]
sealed class DemoInfoAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}

[DemoInfo("Attributes add metadata that reflection can read.")]
sealed class DemoFeature
{
}

sealed class DemoResource : IDisposable
{
    public void Dispose() => Console.WriteLine("IDisposable: cleaning up the example resource.");
}
