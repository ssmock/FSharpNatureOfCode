namespace Extensions

type Vector2 (x: float, y: float) =
    member __.X = x
    member __.Y = y

    member this.X' x =
        Vector2 (x, this.Y)

    member this.Y' y =
        Vector2 (this.X, y)

    static member private Bothwise op (v: Vector2): Vector2 =
        Vector2 (op v.X, op v.Y)

    static member private Pairwise op (v1: Vector2) (v2: Vector2): Vector2 =
        Vector2 (op v1.X v2.X, op v1.Y v2.Y)

    static member private Sum (v: Vector2) =
        v.X + v.Y

    static member private SqrtSum (v: Vector2) =
        Vector2.Sum v
        |> sqrt

    static member private Sq f = f * f

    member this.MagSq =
        Vector2.Bothwise Vector2.Sq this
        |> Vector2.Sum

    member this.Mag =
        this.MagSq
        |> sqrt

    member this.Normalized =
        if this.Mag = 0. then Vector2 (0., 0.)
        else Vector2 ( this.X / this.Mag,
                       this.Y / this.Mag )

    static member Distance (v1: Vector2, v2: Vector2) =
        Vector2.Pairwise (-) v1 v2
        |> Vector2.Bothwise (( ** ) 2.)
        |> Vector2.Sum

    static member (|->) (v1: Vector2, v2: Vector2) = Vector2.Distance

    static member (+) (v1: Vector2, v2: Vector2) = 
        Vector2.Pairwise (+) v1 v2

    static member (-) (v1: Vector2, v2: Vector2) = 
        Vector2.Pairwise (-) v1 v2

    static member (*) (v: Vector2, x: float) = 
        Vector2.Bothwise ((*) x) v

    // Convenient, but weird
    static member (*) (v1: Vector2, (x, y)) =
        Vector2.Pairwise (*) v1 (Vector2(x,y))

    static member (/) (v: Vector2, x: float) =
        Vector2.Bothwise ((/) x) v

    static member (.*) (v1: Vector2, v2: Vector2) =
        Vector2.Pairwise (*) v1 v2
        |> Vector2.Sum
