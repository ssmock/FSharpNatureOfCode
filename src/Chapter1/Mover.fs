namespace Chapter1

open Extensions
open Processing

type WorldParams = {
    LowerBound: Vec2
    UpperBound: Vec2
}

type Mover = {
    Location: Vec2
    Velocity: Vec2
    Acceleration: WorldParams -> Vec2
    MaxVelocity: float
}

module MoverBehavior =

    /// Gets a new velocity based on the given bounding values,
    /// producing a "ricochet" behavior
    let GetRicochetVelocity (eval: Vec2BoundsEval) (vel: Vec2) =
        let x = match eval.X with
                | High | Low -> vel.X * -1.
                | _ -> vel.X 

        let y = match eval.Y with
                | High | Low -> vel.Y * -1.
                | _ -> vel.Y

        Vec2(x, y)

    /// Gets a new location based on the bounding values,
    /// producting a "wrap" behavior
    let GetWrappedLocation (eval: Vec2BoundsEval) (loc: Vec2): Vec2 =
        let x = match eval.X with
                | High | Low -> 0.
                | _ -> loc.X

        let y = match eval.Y with
                | High | Low -> 0.
                | _ -> loc.Y

        Vec2(x, y)

module VelocityMoverWorld =

    let UpdateMover (worldParams: WorldParams) mover =
        let newLoc = mover.Location + mover.Velocity
        let eval, _clamped = Bounds.ClampVec2 worldParams.UpperBound worldParams.LowerBound newLoc
        
        { mover with
                Location = MoverBehavior.GetWrappedLocation eval newLoc }

module AccelerationMoverWorld =

    let UpdateWorld (p: P5) (w: WorldParams): WorldParams =
        w

    let UpdateMover (worldParams: WorldParams) mover =
        let newVel = (mover.Velocity + mover.Acceleration worldParams)
                     |> Vec2.Limit mover.MaxVelocity

        let newLoc = mover.Location + newVel

        let eval, _clamped = Bounds.ClampVec2 worldParams.UpperBound worldParams.LowerBound newLoc
        
        { mover with
                Velocity = newVel
                Location = MoverBehavior.GetWrappedLocation eval newLoc }
