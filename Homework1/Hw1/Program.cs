namespace Hw1;
public class Program
{
    public static void Main(string[] args)
    {
        Parser.ParseCalcArguments(args, out var arg1, out var operation, out var arg2);

        // TODO: implement calculator logic
        var result = Calculator.Calculate(arg1, operation, arg2);
        Console.WriteLine(result);
    }
}