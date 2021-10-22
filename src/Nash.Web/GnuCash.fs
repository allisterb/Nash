namespace Nash.Web


open System
open System.Collections.Generic;
open System.Linq

open FSharp.Control

open WebSharper
open Npgsql.FSharp
open Humanizer
open Newtonsoft.Json

open Nash
open Nash.Models

module GnuCash =

    let private pgdb =
        Sql.host (Runtime.Config("PGSQL"))
        |> Sql.port 5432
        |> Sql.username "gnucash"
        |> Sql.password "gnucash"
        |> Sql.database "gnucash"
        |> Sql.sslMode SslMode.Prefer
        |> Sql.config "Pooling=true"
        |> Sql.formatConnectionString
        |> Sql.connect

    [<Rpc>]
    let getAcccounts() =
        pgdb
        |> Sql.query "SELECT * FROM public.accounts"
        |> Sql.executeAsync (fun read -> {
            Guid =  read.string("guid")
            Name = read.string("name")
            Type = read.string("account_type") |> AccountType.Parse
            ParentGuid = read.stringOrNone("parent_guid") |> function | Some g -> g | _ -> ""
            Description = read.string("description")
            
        }) 
        |> Async.map(
            function 
            | Ok a  -> (if a.Length > 0 then infof "Retrieved {0} accounts from database." [a.Length]; Some a else None) 
            | Error exn -> errex "Error retrieving accounts from database." exn []; None)

    let accounts = 
        match Async.RunSynchronously(getAcccounts()) with
        | Some a -> a
        | None -> failwith ""

    [<Rpc>]
    let getaccountTypes() = accounts |> List.map(fun a -> a.Type.ToString()) |> List.distinct

    [<Rpc>]
    let getAccountsByCategory(category:string) = accounts |> List.filter(fun a -> a.Type.ToString().ToUpper() = category.ToUpper() && not (a.Name.StartsWith("Imbalance")))