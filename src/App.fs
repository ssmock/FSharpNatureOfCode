module App

open Fable.Core
open Fable.Core.JsInterop
open Processing

let _p5Import: obj = importAll "p5"
let _p5Integration: obj = importAll "./Processing/js/integration"

let init () =
  let setup = Introduction.RandomWalkers.setup
  let draw = Introduction.RandomWalkers.draw

  P5.StartProcessing ()  

init ()
