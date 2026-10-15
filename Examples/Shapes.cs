namespace CSharpConsoleApp.Examples;

internal abstract class Shape
{
    public abstract double Area();
}

internal sealed class Circle(double radius) : Shape
{
    public double Radius { get; } = radius;

    public override double Area() => Math.PI * Radius * Radius;
}

internal sealed class Rectangle(double width, double height) : Shape
{
    public double Width { get; } = width;
    public double Height { get; } = height;

    public override double Area() => Width * Height;
}
