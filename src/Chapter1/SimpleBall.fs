namespace Chapter1

open Processing
open Fable.Core.JsInterop
open Extensions

module SimpleBall =
    type Constant = {
        Bounds: Vec2
    }

    type State = {
        BallPosition: Vec2
        BallVelocity: Vec2
        BallRadius: float
    }

    let constant = {
        Bounds = new Vec2 (720., 720.)
    }

    let mutable private state = {
        BallPosition = new Vec2 (0., 0.)
        BallRadius = 25.
        BallVelocity = new Vec2 (1.5, 1.2)
    } 

    let private drawBall (p: P5) (s: State) =
        p.ellipse s.BallPosition.X s.BallPosition.Y s.BallRadius s.BallRadius

    let isOutOfBounds x bound =
        x > bound || x < 0.

    let getEffectiveVelocity () =    
        let effectiveVelocityX = if isOutOfBounds state.BallPosition.X constant.Bounds.X then
                                    state.BallVelocity.X * -1.
                                 else state.BallVelocity.X
        let effectiveVelocityY = if isOutOfBounds state.BallPosition.Y constant.Bounds.Y then
                                    state.BallVelocity.Y * -1.
                                 else state.BallVelocity.Y

        Vec2 (effectiveVelocityX, effectiveVelocityY)

    let private updateState () =
        let effectiveVelocity = getEffectiveVelocity ()

        state <- { state with 
                         BallPosition = state.BallPosition + effectiveVelocity
                         BallVelocity = effectiveVelocity }

    let Setup (p: P5) =
        p.createCanvas (int constant.Bounds.X)
                       (int constant.Bounds.Y)
        p.background !^ 0

    let Draw (p: P5) =
        updateState ()

        p.background !^ 0
        drawBall p state