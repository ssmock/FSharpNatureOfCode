namespace Walkers

module Walkers2D =
    open Fable.Core.JsInterop
    open Processing

    [<Literal>] 
    let DefaultWalkerRadius = 1.

    type Walker = {
        x:float
        y:float
        radius:float
        fillColor:string
    }

    let makeWalkerAtPoint x y color radius =
        { 
            x = x 
            y = y
            radius = radius
            fillColor = color
        }

    let makeDefaultWalker () = 
        makeWalkerAtPoint 0. 0. "#000" DefaultWalkerRadius

    let makeWalkerAtMiddle (p: P5) color radius =
        makeWalkerAtPoint (p.width / 2.) (p.height / 2.) color radius

    let drawWalker (p: P5) (w: Walker) =
        p.stroke !^ w.fillColor
        p.fill !^ w.fillColor
        p.ellipse w.x w.y w.radius w.radius