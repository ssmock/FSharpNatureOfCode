namespace Extensions

module F5 =
    open Fable.Core.JsInterop
    open Processing

    type InitializeState<'a> = unit -> 'a
    type PrepareCanvas<'a> = P5 -> 'a -> unit
    type Cycle<'a> = P5 -> 'a -> 'a

    let _p5Import: obj = importAll "p5"
    let _p5Integration: obj = importAll "../Processing/js/integration"

    let Start (initState: InitializeState<'a>) (prepCanvas: PrepareCanvas<'a>) (cycle: Cycle<'a>) =
        let mutable s = initState ()
        
        let setup (p: P5) =
            prepCanvas p s

        let draw (p: P5) =
            s <- cycle p s

        P5.StartProcessing ()