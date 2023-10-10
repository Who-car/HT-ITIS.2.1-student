module Hw5.Parser

open System
open Hw5.Calculator

let isArgLengthSupported (args:string[]): Result<(string * string * string), Message> =
    if args.Length = 3 then Ok (args[0], args[1], args[2]) else Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (operation): Result<CalculatorOperation, Message> =
    match operation with
    | Calculator.plus -> Ok CalculatorOperation.Plus
    | Calculator.minus -> Ok CalculatorOperation.Minus
    | Calculator.multiply -> Ok CalculatorOperation.Multiply
    | Calculator.divide -> Ok CalculatorOperation.Divide
    | _ -> Error Message.WrongArgFormatOperation
    
let parseNum (arg: string) =
    match Double.TryParse(arg) with
    | true, result -> Ok result
    | _ -> Error Message.WrongArgFormat

let parseArgs (args: string[]): Result<(double * CalculatorOperation * double), Message> =
    MaybeBuilder.maybe {
        let! arg1, op, arg2 = isArgLengthSupported args
        let! value1 = parseNum arg1
        let! operation = isOperationSupported op
        let! value2 = parseNum arg2
        return (value1, operation, value2)
    }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<(double * CalculatorOperation * double), Message> =
    if operation = CalculatorOperation.Divide && arg2 = 0.0
    then Error Message.DivideByZero
    else Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    MaybeBuilder.maybe {
        let! args = parseArgs args
        let! args = isDividingByZero args
        return args
    }