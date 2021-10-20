// Contains code from FsHtml: https://github.com/ptrelford/FsHtml by Phillip Trelford
// FsHtml is licensed under the Apache License 2.0: https://raw.githubusercontent.com/ptrelford/FsHtml/master/LICENSE 

namespace Nash.Web

open System

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.UI.Client
open WebSharper.UI.Html
open WebSharper.JQuery
open WebSharper.SweetAlert

open SMApp.JQueryTerminal
open SMApp.WebSpeech
open SMApp.TypingDNA

type TimerTemplate = Templating.Template<"wwwroot/Timer.html">

[<AutoOpen; JavaScript>]
module ClientExtensions =
    let rnd = System.Random()

    let rawOpt = EchoOptions(Raw=true)

    type Terminal with
        member x.Echo' (text:string) = x.Pause(); x.Echo text; x.Resume()
        member x.EchoHtml' (text:string) = x.Pause(); x.Echo(text, rawOpt) ; x.Resume()
      
    let toArray (a : ArrayLike<'t>) =
        JQuery.MakeArray a |> Array.map (fun a -> a :?> 't)

    let jserror = JQuery.JQuery.Error 

    let info = Console.Info
    
    let error = Console.Error

    let debug (loc:string) t = info <| sprintf "DEBUG: %s: %A" (loc.ToUpper()) t
    
    let toLower (s:string) = s.ToLower()

    [<Direct "$('#term').terminal().disable()">]
    let disableTerminal() = X<unit>

    [<Direct "$('#term').terminal().enable()">]
    let enableTerminal() = X<unit>

    let terminalOutput() = JQuery(".terminal-output").Get().[0]

    [<Direct "$('#term').terminal()">]
    let Terminal = X<Terminal>

    let getMic() = JQuery("#microphone").Get().[0]

    [<Direct("window.lastMicData")>]
    let lastMicData() = X<Int16Array>

    [<Direct("window.speechSynthesis")>]
    let speechSynthesis() = X<SpeechSynthesis>

    let eid = attr.id
    
    let reid s = s + "-" + rnd.Next().ToString()  |> eid

    let cls n = attr.``class`` n
    
    let href s = attr.href s

    let dindex (n:int) = Attr.Create "data-index" (n.ToString())
    
    let container c = div [cls "container"] c
    
    let getContainer() = JQuery("#container").Get().[0].FirstChild |> As<Dom.Element>
    
    let createElement doc =
        let el = JS.Document.CreateElement "div"
        do JS.Document.AppendChild(el) |> ignore        
        do doc |> Doc.RunAppend el
        
    let elementHTML (d:Dom.Element) = d.InnerHTML
    
    let createCanvas (id:string) (width:string) (height:string) (parent:Dom.Element) =
        canvas[eid id; attr.width width; attr.height height][] |> Doc.Run parent
        let c = parent.FirstChild |> As<CanvasElement>
        c

    let createDialogueBoxCanvas() =
        let e = JQuery("#swal2-content").Get().[0].FirstChild |> As<Dom.Element>
        let c = createCanvas "camera" "640" "480" e
        c

    let getDialogueBoxContent() = JQuery("#swal2-content").Get().[0].FirstChild |> As<Dom.Element>
        
    let getDialogueBoxCanvas() = JQuery("canvas.swal2-content").Get().[0] |> As<CanvasElement> 

    let getDialogueBoxInput() = JQuery(".swal2-input").Get().[0] |> As<HTMLInputElement>
    
    [<Direct "startCamera($container, $canvasElement)">]
    let startCamera (container:Dom.Element) (canvasElement:Dom.Element) = X<unit>
    
    [<Direct "stopCamera()">]
    let stopCamera () = X<unit>

    [<Direct "getCameraCanvas()">]
    let getCameraCanvas() = X<CanvasElement>

    type SweetAlert with
        static member QueueBoxes = SweetAlert.Mixin(Box()).Queue

    let questionBox title text (queueSteps: Box[] option) (dim:(int * int) option) (onCreate:(Box->unit) option) (onShow:(unit->unit) option) (onInput:SweetAlertResult<obj>->unit)= 
        let prom = 
            let b = 
                match dim with
                | (Some(width, height)) -> 
                    SweetAlert.Box (
                                TitleText = title,
                                Text = text,
                                Icon = "question",
                                Width = width.ToString(),
                                Html = sprintf "<div class=\"swal2-content-custom\" style=\"width:%ipx;height:%ipx\"></div>" width height,
                                AllowOutsideClick = false
                    )
                | None -> 
                    SweetAlert.Box (
                        TitleText = title,
                        Text = text,
                        Icon = "question",
                        Html = "<div></div>",
                        AllowOutsideClick = false
                    )   
            do if onCreate.IsSome then onCreate.Value b
          
            match queueSteps with
            | None -> b |> SweetAlert.Fire
            | Some s -> 
                b.ProgressSteps <- seq {1..s.Length} |> Array.ofSeq
                (SweetAlert.Mixin b).Queue(Array.create (s.Length) b)
            
        do if onShow.IsSome then onShow.Value()
        prom.Then(Action<SweetAlertResult<obj>>(onInput))
        
    let confirmQuestionBox = SweetAlert.ClickConfirm
         
    let boxesWithTitles (boxes:string array) = boxes |> Array.map(fun b -> SweetAlert.Box(TitleText = b))
    
    let swap (a: _[]) x y =
         let tmp = a.[x]
         a.[x] <- a.[y]
         a.[y] <- tmp

    // shuffle an array (in-place)
    let shuffleArray a = 
        Array.iteri (fun i _ -> swap a i (rnd.Next(i, Array.length a))) a
        a
    
