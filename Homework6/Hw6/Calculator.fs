module Hw6.Calculator

type CalculatorOperation =
     | Plus = 0
     | Minus = 1
     | Multiply = 2
     | Divide = 3
     
let calculate (value1:double, operation:CalculatorOperation, value2:double) =
    match operation with
    | CalculatorOperation.Plus -> Ok $"{value1 + value2}"
    | CalculatorOperation.Minus -> Ok $"{value1 - value2}"
    | CalculatorOperation.Multiply -> Ok $"{value1 * value2}"
    | CalculatorOperation.Divide -> if value2 = 0 then Ok "DivideByZero" else Ok $"{value1 / value2}"
    | _ -> Error "Invalid operation"