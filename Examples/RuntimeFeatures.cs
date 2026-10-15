namespace CSharpConsoleApp.Examples;

internal static class StringExtensions
{
    public static string ToTopicLabel(this string value) => $"[{value}]";
}

[AttributeUsage(AttributeTargets.Class)]
internal sealed class DemoInfoAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}

[DemoInfo("Attributes add metadata that reflection can read.")]
internal sealed class DemoFeature
{
}

internal sealed class DemoResource : IDisposable
{
    public void Dispose() => Console.WriteLine("IDisposable: cleaning up the example resource.");
}
