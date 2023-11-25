using Hw11.Dto;

namespace Hw11.Services.MathCalculator;

public interface IMathCalculatorService
{
    public Task<double> CalculateMathExpressionAsync(string? expression);
}