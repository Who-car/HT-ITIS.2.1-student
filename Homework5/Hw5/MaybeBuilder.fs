module Hw5.MaybeBuilder

open System

type MaybeBuilder() =
    member builder.Bind(a, f): Result<'e,'d> =
        match a with
        | Error e -> Error e
        | Ok x -> f x
    member builder.Return x: Result<'a,'b> =
        Ok x
let maybe = MaybeBuilder()