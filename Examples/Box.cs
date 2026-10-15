namespace CSharpConsoleApp.Examples;

internal sealed class Box<T>(T value)
{
    public T Value { get; } = value;
}
