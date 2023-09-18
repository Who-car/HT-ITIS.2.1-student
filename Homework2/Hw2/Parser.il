.assembly extern mscorlib {}

.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public static void ParseCalcArguments(string[], [out] float64&, [out] valuetype Hw2.CalculatorOperation&, [out] float64&) cil managed
    {
        //Проверяем первый параметр на количество введенных аргументов
        .maxstack 4
        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse LengthNotSupported
        
        //Проверяем первое число на валидность
        ldarg.0
        ldc.i4 0
        ldelem.ref
        ldarg.1
        call void Hw2.Parser::ParseNum(string, float64&)
        
        //Проверяем операцию на валидность
        ldarg.2
        ldarg.0
        ldc.i4 1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        
        //Проверяем второе число на валидность
        ldarg.0
        ldc.i4 2
        ldelem.ref
        ldarg.3
        call void Hw2.Parser::ParseNum(string, float64&)
        ret
        
        LengthNotSupported:
        ldstr "Неверное количество аргументов. Пожалуйста, введите одно число, одну из операций +, -, *, /, одно число"
        newobj instance void [mscorlib]System.ArgumentException::.ctor(string)
        throw
    }
    
    .method private static bool IsArgLengthSupported(string[]) cil managed
    {
        //Проверяем длину массива (должна равняться трём)
        ldarg.0
        ldlen
        ldc.i4 3
        ceq
        ret
    }
    
    .method public static valuetype Hw2.CalculatorOperation ParseOperation(string) cil managed
    {
        //Проверяем соответствие введённой операции CalculatorOoperation
        ldarg.0
        ldstr "+"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Plus
        
        ldarg.0
        ldstr "-"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Minus
        
        ldarg.0
        ldstr "*"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Mul
        
        ldarg.0
        ldstr "/"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue.s Div
        br Undefined

        Plus: 
        ldc.i4 0
        br Success

        Minus:
        ldc.i4 1
        br Success

        Mul:
        ldc.i4 2
        br Success

        Div:
        ldc.i4 3
        br Success

        Undefined:
        ldc.i4 4
        br Success

        Success:
        ret
    }
    
    .method private static void ParseNum(string, [out] float64&) cil managed
    {
        //Пытаемся преобразовать строку в число типа double
        //в случае неудачи выбрасываем исключение
        ldarg.0
        ldarg.1
        call bool [mscorlib]System.Double::TryParse(string, float64&)
        brfalse.s Fail
        ret
        
        Fail:
        ldstr "Пожалуйста, вводите только цифры"
        newobj instance void [mscorlib] System.ArgumentException::.ctor(string)
        throw
    }
}


