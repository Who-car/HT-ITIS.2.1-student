module Hw4.Parser

open System
open Hw4.Calculator

type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    args.Length = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> InvalidOperationException "Операция не распознана. Вводите только +, -, *, /" |> raise
    
let parseNum (arg: string) =
    let mutable result = 0.0
    if Double.TryParse(arg, &result) then result
    else ArgumentException "Пожалуйста используйте только цифры" |> raise
        
    
let parseCalcArguments(args : string[]) =
    if isArgLengthSupported args then {
        arg1 = parseNum args[0]
        operation =  parseOperation args[1]
        arg2 = parseNum args[2]
    }
    else ArgumentException "Неверное количество аргументов. Введите число, одну из операций +, -, *, /, число" |> raise