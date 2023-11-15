module AsyncEitherClient.AsyncEitherBuilder

open Microsoft.FSharp.Control

type AsyncEither<'a, 'b> = Async<Result<'a, 'b>>

type AsyncEitherBuilder() =
    member this.Bind(m, k) =
        async {
            let! res = m
            match res with
            | Ok value -> return! k value
            | Error error -> return Error error
        }

    member this.Return(value) :Async<Result<'a, 'b>> =
        async {
            return Ok value
        }

let asyncEither = AsyncEitherBuilder()