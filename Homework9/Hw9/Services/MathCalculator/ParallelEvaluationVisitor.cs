using System.Linq.Expressions;
using Hw9.ErrorMessages;

namespace Hw9.Services.MathCalculator;

public class ParallelEvaluationVisitor : ExpressionVisitor
{
    public static Task<Expression> VisitExpression(Expression expression)
    {
        return Task.Run(() => new ParallelEvaluationVisitor().Visit(expression));
    }
    
    protected override Expression VisitBinary(BinaryExpression node)
    {
        return Expression.Constant(VisitBinaryAsync(node).Result);
    }

    private async Task<Expression> VisitBinaryAsync(BinaryExpression node)
    {
        var firstTask = new Lazy<Task<ConstantExpression>>(async () =>
        {
            await Task.Delay(1000);
            if (node.Left is BinaryExpression binaryLeft)
                return Expression.Constant(await VisitBinaryAsync(binaryLeft));
            return Expression.Constant(node.Left);
        });
        var secondTask = new Lazy<Task<ConstantExpression>>(async () =>
        {
            await Task.Delay(1000);
            if (node.Right is BinaryExpression binaryLeft)
                return Expression.Constant(await VisitBinaryAsync(binaryLeft));
            return Expression.Constant(node.Right);
        });

        var result = await Task.WhenAll(firstTask.Value, secondTask.Value);

        return node.NodeType switch
        {
            ExpressionType.Add => Expression.Add(result[0], result[1]),
            ExpressionType.Subtract => Expression.Subtract(result[0], result[1]),
            ExpressionType.Multiply => Expression.Multiply(result[0], result[1]),
            ExpressionType.Divide => result[1].Value!.Equals(0) 
                ? throw new Exception(MathErrorMessager.DivisionByZero) 
                : Expression.Divide(result[0], result[1]),
            _ => throw new InvalidOperationException("Operation not supported")
        };
    }
}