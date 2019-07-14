namespace Extensions

/// Somtimes eases construction; prefer Vec2
type Vec2Rec = { X: float; Y: float }

type Vec2 (x: float, y: float) =
    member __.X = x
    member __.Y = y

    member this.X' x =
        Vec2 (x, this.Y)

    member this.Y' y =
        Vec2 (this.X, y)

    static member Unit = Vec2(1., 1.)

    static member FromRec r = Vec2(r.X, r.Y)

    static member private Bothwise op (v: Vec2): Vec2 =
        Vec2 (op v.X, op v.Y)

    static member private Pairwise op (v1: Vec2) (v2: Vec2): Vec2 =
        Vec2 (op v1.X v2.X, op v1.Y v2.Y)

    static member private Sum (v: Vec2) =
        v.X + v.Y

    static member private SqrtSum (v: Vec2) =
        Vec2.Sum v
        |> sqrt

    static member private Sq f = f * f

    member this.MagSq =
        Vec2.Bothwise Vec2.Sq this
        |> Vec2.Sum

    member this.Mag =
        this.MagSq
        |> sqrt

    member this.Normalized =
        if this.Mag = 0. then Vec2 (0., 0.)
        else Vec2 ( this.X / this.Mag,
                       this.Y / this.Mag )

    static member Distance (v1: Vec2, v2: Vec2) =
        Vec2.Pairwise (-) v1 v2
        |> Vec2.Bothwise (( ** ) 2.)
        |> Vec2.Sum

    static member (|->) (v1: Vec2, v2: Vec2) = Vec2.Distance

    static member (+) (v1: Vec2, v2: Vec2) = 
        Vec2.Pairwise (+) v1 v2

    static member (-) (v1: Vec2, v2: Vec2) = 
        Vec2.Pairwise (-) v1 v2

    static member (*) (v: Vec2, x: float) = 
        Vec2.Bothwise ((*) x) v

    // Convenient, but weird
    static member (*) (v1: Vec2, (x, y)) =
        Vec2.Pairwise (*) v1 (Vec2(x,y))

    static member (/) (v: Vec2, x: float) =
        Vec2.Bothwise ((/) x) v

    static member (.*) (v1: Vec2, v2: Vec2) =
        Vec2.Pairwise (*) v1 v2
        |> Vec2.Sum
