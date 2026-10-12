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

Console.WriteLine("Simple Q&A Bot");
Console.WriteLine("Ask me a question, type 'help' for examples, or type 'exit' to quit.");

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
