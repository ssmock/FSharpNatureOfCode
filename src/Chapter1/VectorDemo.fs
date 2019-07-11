namespace Chapter1

open Processing
open Fable.Core.JsInterop
open Extensions

module VectorDemo =
    type Constant = {
        Bounds: Vector2
        Shift: Vector2
    }

    type State = {
        Target: Vector2
    }

    let constant = {
        Bounds = Vector2 (720., 720.)
        Shift = Vector2 (360., 360.)
    }

    let mutable state = {
        Target = Vector2 (0., 0.)
    } 

    let updateState (p: P5) =
        state <- { state with
                         Target = Vector2 (p.mouseX, p.mouseY) }

    let drawLine (p: P5) =
        p.stroke !^ 255
        let adjustedTarget = (state.Target - constant.Shift).Normalized * 100.
        
        p.translate constant.Shift.X constant.Shift.Y

        p.line 0. 0.
               adjustedTarget.X adjustedTarget.Y

    let drawMagnitude (p: P5) =
        p.stroke !^ 255
        let adjustedTarget = state.Target - constant.Shift
        p.line 0. 10. adjustedTarget.Mag 10.

    let Setup (p: P5) =
        p.createCanvas (int constant.Bounds.X)
                       (int constant.Bounds.Y)
        p.background !^ 0

    let Draw (p: P5) =
        updateState p

        p.background !^ 0
        
        drawMagnitude p
        drawLine p