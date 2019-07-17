namespace Chapter1

open Fable.Core.JsInterop

open Constants
open Processing
open Extensions
open AccelerationMoverWorld

module MoverSketch =

    let constantAcceleration = fun (w: WorldParams) -> Vec2(0.02, 0.015)

    type State = {
        Mover: Mover
        World: WorldParams
    }

    let InitState () =
        { Mover = { Location = Vec2(0., 30.)
                    Velocity = Vec2(0., 0.)
                    MaxVelocity = 10. 
                    Acceleration = constantAcceleration }
          World = { LowerBound = Vec2(0., 0.)
                    UpperBound = Vec2(600., 600.) } }

    let PrepCanvas (p: P5) s =
        p.createCanvas (int s.World.UpperBound.X)
                       (int s.World.UpperBound.Y)
        p.background !^ 0
      
    let Cycle (p: P5) s =
        p.background !^ 0

        let w' = UpdateWorld p s.World
        let m' = UpdateMover w' s.Mover

        p.stroke !^ "#800"
        p.fill !^ (if p.keyIsDown KeyCodes.Up then "#047" else "#074")

        p.ellipse m'.Location.X
                  m'.Location.Y
                  25.
                  25.

        { World = w'; Mover = m' }
    