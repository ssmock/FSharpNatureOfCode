namespace Introduction

module RandomWalkers =
    open Fable.Core.JsInterop
    open Processing
    open Walkers.Walkers2D

    let mutable walkerConfig : list<Walker * (P5 -> Walker -> Walker)> = [];

    let setup (p: P5) =
        p.createCanvas 720 720
        p.background !^ 0
        
        walkerConfig <- [
            makeWalkerAtMiddle p "rgba(0,255,0,.15)" 1., WalkerTransforms.simpleRandom
            makeWalkerAtMiddle p "rgba(255,255,0,.15)" 1., WalkerTransforms.doubleRandom
            makeWalkerAtMiddle p "rgba(255,0,0,.15)" 1., WalkerTransforms.downRightRandom
            makeWalkerAtMiddle p "rgba(0,90,200,.15)" 1., WalkerTransforms.chaseMouse
            makeWalkerAtMiddle p "rgba(170,120,0,.15)" 1., WalkerTransforms.maybeTowardMouse
            makeWalkerAtMiddle p "rgba(90,0,20,.15)" 1., WalkerTransforms.gaussian
            makeWalkerAtMiddle p "rgba(120,90,10,.05)" 7., WalkerTransforms.monteCarloBig
            makeWalkerAtMiddle p "rgba(10,90,120,.05)" 7., WalkerTransforms.monteCarloSmall
            makeWalkerAtMiddle p "rgba(10,120,10,.05)" 7., WalkerTransforms.monteCarloSquare
            makeWalkerAtMiddle p "rgba(220,0,255,.15)" 1., WalkerTransforms.noisePosition
            makeWalkerAtMiddle p "rgba(180,180,60,.15)" 1., WalkerTransforms.noiseDistance
        ]

    let draw (p:P5) =
        let transformWalker pair =
            let w, tran = pair
            tran p w, tran

        let drawWalker pair =
            let w, _ = pair
            drawWalker p w

        walkerConfig <-
            walkerConfig
            |> List.map transformWalker
        
        walkerConfig
        |> List.iter drawWalker
