.assembly extern mscorlib {}

.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public static float64 Calculate(float64, valuetype Hw2.CalculatorOperation, float64)
    {
        //В зависимости от введённой операции выполняем соответствующие действия
        //Если операция undefined, то выбрасываем исключение
        ldarg.1
        ldc.i4.0
        ceq
        brtrue.s Plus
        
        ldarg.1
        ldc.i4.1
        ceq
        brtrue.s Minus
        
        ldarg.1
        ldc.i4.2
        ceq
        brtrue.s Mul
        
        ldarg.1
        ldc.i4.3
        ceq
        brtrue.s Div
        br.s Fail
        
        Plus:
        ldarg.0
        ldarg.2
        add
        br.s Success
        
        Minus:
        ldarg.0
        ldarg.2
        sub
        br.s Success
        
        Mul:
        ldarg.0
        ldarg.2
        mul
        br.s Success
        
        Div:
        ldarg.0
        ldarg.2
        div
        br.s Success
        
        Fail:
        ldstr "Операция не распознана. Пожалуйста вводите только одну из операций +, -, *, /"
        newobj instance void [mscorlib] System.InvalidOperationException::.ctor(string)
        throw
        
        Success:
        ret
    }
}



