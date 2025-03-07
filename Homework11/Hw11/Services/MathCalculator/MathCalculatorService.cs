using System.Linq.Expressions;
using Hw11.Dto;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        MathExpressionValidatorService.ValidateExpression(expression);
        dynamic tree = MathParserService.ParseExpression(expression!);
        var result = await ExpressionVisitorDispatcher.ExpressionVisitorDispatcher.Visit(tree);
            
        return result;
    }
}