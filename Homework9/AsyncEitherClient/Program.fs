module AsyncEitherClient.App

open AsyncEitherClient.AsyncEitherBuilder
open System.Net.Http

let runAsyncEither (action: Async<Result<'a, 'b>>) =
    async {
        let! res = action
        return res
    } 

let sendRequestAsync (url: string) =
    async {
        let httpClient = new HttpClient()
        try
            let! response = httpClient.GetAsync(url) |> Async.AwaitTask
            return Ok (response.Content.ReadAsStringAsync().Result)
        with
           | ex -> return Error ex.Message
    }

let test =
    asyncEither {
            let! response1 = sendRequestAsync "https://api.example.com/endpoint1"
            let! response2 = sendRequestAsync "https://api.example.com/endpoint1"
            return $"First response result: {response1};\nSecond response result: {response2}."
        }

let main =
    async {
        let! result = test |> runAsyncEither

        match result with
        | Ok value -> printfn "Result: %s" value
        | Error error -> printfn "Error: %s" error
    }

main |> Async.RunSynchronously