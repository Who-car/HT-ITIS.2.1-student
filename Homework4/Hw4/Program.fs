open System
open Hw4.Calculator
open Hw4.Parser
printfn "Введите число, одну из операций +, -, *, /, число"
let input = Console.ReadLine().Split [|' '|]
let ops = parseCalcArguments input
let result = calculate ops.arg1 ops.operation ops.arg2
printfn $"%f{result}"