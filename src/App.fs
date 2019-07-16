module App

open Fable.Core.JsInterop

let init () =
  30., 60.

let first (p: Processing.P5) _ =
  p.createCanvas 720 720
  p.background !^ 0

let cycle (p: Processing.P5) (x, y) =
  p.ellipse x y x y
  x + 1., y + 1.

Extensions.F5.Start init
                    first
                    cycle