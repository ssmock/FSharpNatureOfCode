namespace Introduction

module WalkerTransforms =
    open Fable.Import.JS
    open Processing
    open Walkers.Walkers2D

    let floor = Math.floor >> int

    let simpleRandom (p:P5) (w:Walker) =
        let choice = floor (p.random 4.)

        let (xInc, yInc) =
            match choice with
            | 0 -> (1., 0.)
            | 1 -> (-1., 0.)
            | 2 -> (0., 1.)
            | _ -> (0., -1.) // 4
        
        { w with
            x = w.x + xInc
            y = w.y + yInc
        }

    let doubleRandom (p:P5) (w:Walker) =
        let getAdjustment () = p.randomFromRange -1. 1.

        { w with
            x = w.x + getAdjustment ()
            y = w.y + getAdjustment ()
        }

    // For exercise 1.1
    let downRightRandom (p:P5) (w:Walker) =
        let pickX roll =
            match roll with
            | _ when roll < 0.6 -> 1.
            | _ -> -1.

        let pickY roll =
            match roll with
            | _ when roll < 0.6 -> 1.
            | _ -> -1.

        let xRoll = p.random 1.
        let yRoll = p.random 1.

        { w with
            x = w.x + (pickX xRoll)
            y = w.y + (pickY yRoll)
        }

    let chaseMouse (p:P5) (w:Walker) =
        let towardMouse () =
            (p.lerp w.x p.mouseX 0.01,
             p.lerp w.y p.mouseY 0.01)

        let (x, y) = towardMouse()
        
        { w with
            x = x
            y = y
        }

    let maybeTowardMouse (p:P5) (w:Walker) =
        let roll = p.random 1.
        
        let towardMouse () =
            (p.lerp w.x p.mouseX 0.01,
             p.lerp w.y p.mouseY 0.01)

        let wander () =
            (w.x + p.randomFromRange -1. 1., 
             w.y + p.randomFromRange -1. 1.)

        let (x, y) =
            match roll with
            | _ when roll <= 0.1 -> towardMouse ()
            | _ -> wander ()
        
        { w with
            x = x
            y = y
        }

    let withRandomDirection (getDistance:unit->float) (p:P5) (w:Walker) =
        let directionRoll = p.random 1.
        let distance = getDistance ()

        let xDel, yDel =
            match directionRoll with
            | _ when directionRoll <= 0.5 -> distance, 0.
            | _                            -> 0., distance

        { w with
            x = w.x + xDel
            y = w.y + yDel
        }

    let gaussian (p:P5) (w:Walker) =
        let getDistance () = p.randomGaussian 0. 2.
        
        withRandomDirection getDistance p w

    // The greater the distance, the more likely it is to be picked
    let monteCarloBig (p:P5) (w:Walker) =
        let rec pickDistance () =
            let distanceRoll = p.randomFromRange -10. 10.
            let qualifier = p.random 10.

            if qualifier <= (Math.abs distanceRoll) 
            then distanceRoll
            else pickDistance ()
            
        withRandomDirection pickDistance p w

    // The smaller the distance, the more likely it is to be picked
    let monteCarloSmall (p:P5) (w:Walker) =
        let rec pickDistance () =
            let distanceRoll = p.randomFromRange -10. 10.
            let qualifier = p.random 10.

            if qualifier >= (Math.abs distanceRoll) 
            then distanceRoll
            else pickDistance ()
            
        withRandomDirection pickDistance p w

    // For exercise 1.6. This is a variant on the big walker.
    let monteCarloSquare (p:P5) (w:Walker) =
        let rec pickDistance () =
            let distanceRoll = p.randomFromRange -10. 10.
            let qualifier = p.random 10.

            if qualifier <= distanceRoll ** 2. 
            then distanceRoll
            else pickDistance ()
            
        withRandomDirection pickDistance p w

    let mutable noisePositionWalkerNoiseSample = (0., 10000.)
    let noisePosition (p:P5) (w:Walker) =
        let xSample, ySample = noisePositionWalkerNoiseSample
        
        let x = p.map (p.noise xSample) 0. 1. 0. p.width
        let y = p.map (p.noise ySample) 0. 1. 0. p.height

        noisePositionWalkerNoiseSample <- (xSample + 0.01, ySample + 0.01)

        { w with x = x; y = y }

    // Really strange one. Because changes in both X and Y are coming from the
    // same distribution, the walker appears to stay close to a diagonal from
    // 0,0 to width,height.
    let mutable noiseDistanceWalkerNoiseSample = 20000.
    let noiseDistance (p:P5) (w:Walker) =
        let pickDistance () =    
            noiseDistanceWalkerNoiseSample <- noiseDistanceWalkerNoiseSample + 0.01
            p.map (p.noise noiseDistanceWalkerNoiseSample) 0. 1. -4. 4.

        withRandomDirection pickDistance p w