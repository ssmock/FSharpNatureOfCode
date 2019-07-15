namespace Extensions

type BoundsEval =
    | Low
    | Ok
    | High

type Vec2BoundsEval = {
    X: BoundsEval
    Y: BoundsEval
}

module Bounds =

    let Clamp upper lower v =
        if v > upper then
            High, upper
        elif v < lower then
            Low, lower
        else
            Ok, v

    let ClampVec2 (upper: Vec2) (lower: Vec2) (v: Vec2): Vec2BoundsEval * Vec2 =
        let xEval, x = Clamp upper.X lower.X v.X
        let yEval, y = Clamp upper.Y lower.Y v.Y

        { X = xEval; Y = yEval }, Vec2 (x, y)
