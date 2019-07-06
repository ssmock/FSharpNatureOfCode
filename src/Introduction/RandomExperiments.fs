namespace Introduction

module RandomExperiments =
    open Fable.Core.JsInterop
    open Processing

    let drawGaussianDotCone (p:P5) =
        let color = "rgba(5,80,50,.02)"

        p.noStroke ()
        p.fill !^ color

        let rows = 60
        let rad = 20.

        // Draws a dot at the given vertical position,
        // but with a random horizontal position taken
        // from the specified normal curve.
        let drawGaussianDotAtY y med stdDev =
            let x = p.randomGaussian med stdDev
            p.ellipse x y rad rad

        let drawRow i = 
            let y = i * 20.
            let stdDev = i * 10.
            drawGaussianDotAtY y (p.width / 2.) stdDev

        seq { for i in 0 .. rows -> float i }
            |> Seq.iter (drawRow)    

    let drawSplatter (p: P5) =
        let medX = p.width / 2.
        let medY = p.height / 2.
        let stdDev = p.width / 8.

        let x = p.randomGaussian medX stdDev
        let y = p.randomGaussian medY stdDev

        let color = P5.Rgba {
            R = int <| p.randomGaussian 128. 16.
            G = int <| p.randomGaussian 108. 16.
            B = int <| p.randomGaussian 64. 16.
            A = 0.5
        }

        p.noStroke ()
        p.fill !^ color

        p.ellipse x y 50. 50.

    let setup (p:P5) =
        p.createCanvas 2400 2400
        p.background !^ 0

    let draw (p:P5) =
        // drawGaussianDotCone p
        drawSplatter p