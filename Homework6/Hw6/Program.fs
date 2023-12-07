module Hw6.App

open System
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Hw6.MaybeBuilder
open Hw6.Parser
open Hw6.Calculator

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> = MaybeBuilder.maybe {
            let! args = ctx.TryBindQueryString<calculatorArgs>()
            let! args = parseArgs args
            let! result = calculate args
            return result.ToString()
        }

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "Use //calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
             route "/calculate" >=> calculatorHandler
        ]
        setStatusCode 404 >=> text "Not Found" 
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services
            .AddGiraffe()
            .AddMiniProfiler(fun option -> option.RouteBasePath <- "/profiler")
        |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app
            .UseMiniProfiler()
            .UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0