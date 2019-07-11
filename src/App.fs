module App

open Fable.Core.JsInterop
open Processing

let _p5Import: obj = importAll "p5"
let _p5Integration: obj = importAll "./Processing/js/integration"

let init () =
  let setup = Chapter1.VectorDemo.Setup
  let draw = Chapter1.VectorDemo.Draw

  P5.StartProcessing ()  

init ()
