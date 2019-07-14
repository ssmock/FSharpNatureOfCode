namespace Chapter1

open Extensions

type VelocityMover = {
    Location: Vec2
    Velocity: Vec2
}

type WorldParams = {
    LowerBound: Vec2
    UpperBound: Vec2
}

module VelocityMoverWorld =

    let UpdateMover (worldParams: WorldParams) mover =
        let newLoc = mover.Location + mover.Velocity
        let eval, clamped = Bounds.ClampVec2 worldParams.UpperBound worldParams.LowerBound newLoc

        let velX = match eval.X with
                   | High | Low -> mover.Velocity.X * -1.
                   | _ -> mover.Velocity.X 

        let velY = match eval.Y with
                   | High | Low -> mover.Velocity.Y * -1.
                   | _ -> mover.Velocity.Y

        let newVel: Vec2 = Vec2(velX, velY)

        { mover with
                Location = clamped 
                Velocity = newVel }