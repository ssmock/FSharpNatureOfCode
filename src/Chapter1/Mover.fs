namespace Chapter1

open Extensions

type VelocityMover = {
    Location: Vector2
    Velocity: Vector2
}

type WorldParams = {
    LowerBound: Vector2
    UpperBound: Vector2
}

// TODO -- Replace with a general clamp fn
type BoundsState = {
    XState: BoundStateValue
    YState: BoundStateValue
}
and BoundStateValue =
    | InBounds
    | BeyondLower
    | BeyondUpper

module VelocityMoverWorld =
    let GetBoundsState worldParams (loc: Vector2) =
        let check upper lower x =
            if x < lower then BeyondLower
            elif x > upper then BeyondUpper
            else InBounds
    
        { XState = check worldParams.LowerBound.X 
                         worldParams.UpperBound.X 
                         loc.X

          YState = check worldParams.LowerBound.Y
                         worldParams.UpperBound.Y 
                         loc.Y }
    
    let UpdateMover worldParams mover =
        let newLoc = mover.Location + mover.Velocity
        let bs = GetBoundsState worldParams newLoc 

        if bs.XState = InBounds && bs.YState = InBounds then
            { mover with
                    Location = newLoc }
        elif bs.XState = InBounds then
            { mover with
                    Location = newLoc.Y' mover.Location.Y
                    Velocity = mover.Velocity * (1., -1.) }
        elif bs.YState = InBounds then
            { mover with
                    Location = newLoc.X' mover.Location.X
                    Velocity = mover.Velocity * (-1., 1.) }
        else
            { mover with
                    Velocity = mover.Velocity * -1. }