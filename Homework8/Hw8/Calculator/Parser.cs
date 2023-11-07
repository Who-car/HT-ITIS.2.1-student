using System.Globalization;

namespace Hw8.Calculator;

public static class Parser
{
    private static void ParseOperation(string operation, out Operation op)
    {
        op = operation.ToLower() switch
        {
            "plus" => Operation.Plus,
            "minus" => Operation.Minus,
            "multiply" => Operation.Multiply,
            "divide" => Operation.Divide,
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
    }

    public static void ParseArgs(string arg1, string operation, string arg2,
        out double val1, out Operation op, out double val2)
    {
        if (!Double.TryParse(arg1, NumberStyles.Float, CultureInfo.InvariantCulture, out val1) 
            || !Double.TryParse(arg2, NumberStyles.Float, CultureInfo.InvariantCulture, out val2))
            throw new ArgumentException(Messages.InvalidNumberMessage);
        ParseOperation(operation, out op);
    }
}