using CSharpConsoleApp.Features;

Console.WriteLine("C# Learning Console");
Console.WriteLine("1. Explore C# topics");
Console.WriteLine("2. Use the Q&A bot");
Console.Write("Choose an option: ");

if (Console.ReadLine() == "2")
{
    QuestionBot.Run();
}
else
{
    await TopicTour.RunAsync();
}
