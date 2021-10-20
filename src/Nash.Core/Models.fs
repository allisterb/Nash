namespace Nash

open System

open WebSharper

[<JavaScript>]
module Models =

    type User = {
        Name:string
        LastLoggedIn:DateTime option
    }
    with 
        member x.Create = {Name = ""; LastLoggedIn = None}
        override x.ToString() = x.Name

    type AccountType =
        | BANK
        | INCOME
        | EXPENSE
        | CASH
        | OTHER
    with 
        static member Parse(s:string) =
            match s.ToUpper() with
            | "BANK" -> BANK
            | _ -> OTHER

    type Account = {
        Guid:string
        Name:string
        Type:AccountType
        ParentGuid:string
        Description:string
    }

 
