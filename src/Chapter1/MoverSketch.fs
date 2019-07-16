namespace Chapter1

open Fable.Core.JsInterop

open Processing
open Extensions
open AccelerationMoverWorld

module MoverSketch =

    let constantAcceleration = fun (w: WorldParams) -> Vec2(0.002, 0.015)

    let mutable mover = { Location = Vec2(0., 30.)
                          Velocity = Vec2(0., 0.)
                          MaxVelocity = 10. 
                          Acceleration = constantAcceleration }

    let mutable world = { LowerBound = Vec2(0., 0.)
                          UpperBound = Vec2(1200., 1200.) }

    let Setup (p: P5) =
        p.createCanvas (int world.UpperBound.X)
                       (int world.UpperBound.Y)
        p.background !^ 0
      
    let Draw (p: P5) =
        p.background !^ 0

        world <- UpdateWorld p world
        mover <- UpdateMover world mover

        p.stroke !^ "#800"
        p.fill !^ "#047"

        p.ellipse mover.Location.X
                  mover.Location.Y
                  25.
                  25.

    