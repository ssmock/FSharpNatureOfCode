namespace Chapter1

open Fable.Core.JsInterop

open Processing
open Extensions

module MoverSketch =

    let mutable mover = { Location = Vec2(20., 30.)
                          Velocity = Vec2(12., 2.) }

    let worldParams = { LowerBound = Vec2(0., 0.)
                        UpperBound = Vec2(1200., 1200.) }

    let update = VelocityMoverWorld.UpdateMover worldParams

    let Setup (p: P5) =
        p.createCanvas (int worldParams.UpperBound.X)
                       (int worldParams.UpperBound.Y)
        p.background !^ 0
      
    let Draw (p: P5) =
        mover <- update mover

        p.ellipse mover.Location.X
                  mover.Location.Y
                  25.
                  25.
