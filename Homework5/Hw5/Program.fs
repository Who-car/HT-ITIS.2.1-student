module Hw5.Main

open System

let input = Console.ReadLine().Split[|' '|]
let args = Parser.parseCalcArguments input
match args with
| Ok (a1, op, a2) -> Calculator.calculate a1 op a2 |> printfn "%f"
| Error e ->
    match e with
    | Message.WrongArgLength -> "Неверное количество аргументов"
    | Message.WrongArgFormat -> "Некорректный формат аргументов"
    | Message.WrongArgFormatOperation -> "Нераспознанная операция"
    | Message.DivideByZero -> "Деление на 0"
    |> printfn "%s"