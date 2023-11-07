namespace Hw8.Calculator;

public class Calculator : ICalculator
{
    public double Calculate(string arg1, string operation, string arg2)
    {
        Parser.ParseArgs(arg1, operation, arg2, 
            out var val1, out var op, out var val2);

        return op switch
        {
            Operation.Plus => Plus(val1, val2),
            Operation.Minus => Minus(val1, val2),
            Operation.Multiply => Multiply(val1, val2),
            Operation.Divide => Divide(val1, val2),
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
    }
    
    public double Plus(double val1, double val2)
    {
        return val1 + val2;
    }

    public double Minus(double val1, double val2)
    {
        return val1 - val2;
    }

    public double Multiply(double val1, double val2)
    {
        return val1 * val2;
    }

    public double Divide(double val1, double val2)
    {
        if (val2 == 0) 
            throw new DivideByZeroException(Messages.DivisionByZeroMessage);

        return val1 / val2;
    }
}