namespace Extensions

type Vector2 = decimal * decimal
type Vector3 = decimal * decimal * decimal

module Vector2 =
    let private bothwise op (v: Vector2): Vector2 =
        (fst >> op) v, (snd >> op) v

    let private pairwise op (v1: Vector2) (v2: Vector2): Vector2 =
        op (fst v1) (fst v2), op (snd v1) (snd v2)

    let private sum (v: Vector2) =
        v |> (fun (x, y) -> x + y)

    let private decSq (n: decimal) = n * n

    let private sqrtSum (v: Vector2) =
        sum v
        |> float        
        |> sqrt

    let magSq (v: Vector2) =
        bothwise decSq v

    let mag (v: Vector2) =
        magSq v
        |> sqrtSum

    let dot (v1: Vector2) (v2: Vector2) =
        let x, y = pairwise (*) v1 v2
        x + y

    let dist (v1: Vector2) (v2: Vector2) =
        pairwise (-) v1 v2
        |> bothwise decSq
        |> sum

    let (+) = pairwise (+)

    let (-) = pairwise (-)

    let (*) x = bothwise ((*) x)

    let (/) x = bothwise ((/) x)

