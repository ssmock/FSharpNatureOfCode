namespace Introduction

module NoiseExperiments =
  open Fable.Core.JsInterop
  open Processing

  type SimpleNoisePlotter = {
      color : string
      sample : float
      sampleInterval : float
      sampleOffset : float
      minYScale : float
      maxYScale : float
      xMultiplier : float
  } 

  let plotSimpleNoise (p: P5) plotter =
      let x = plotter.sample * plotter.xMultiplier
      
      let draw () =
          let min = plotter.minYScale * p.height
          let max = plotter.maxYScale * p.height

          let n = p.noise (plotter.sample + plotter.sampleOffset)
          
          let y = p.mapClamp n 0. 1. min max

          p.stroke !^ plotter.color
          p.point x y

      match x with
      | _ when x <= p.width -> 
           draw ()
           { plotter with sample = plotter.sample + plotter.sampleInterval }
      | _ -> plotter

  let mutable noiseSimplePlotters : SimpleNoisePlotter list = []

  let setup (p:P5) =
      p.createCanvas 720 720
      p.background !^ 0
      
      noiseSimplePlotters <- [
          { color = "#f00"
            sample = 0.; sampleOffset = 0.; sampleInterval = 0.02 
            minYScale = 0.; maxYScale = 0.1; xMultiplier = 5. }
          
          { color = "#0f0"
            sample = 0.; sampleOffset = 1000.; sampleInterval = 0.02 
            minYScale = 0.1; maxYScale = 0.2; xMultiplier = 10. }

          { color = "#00f"
            sample = 0.; sampleOffset = 3000.; sampleInterval = 0.05 
            minYScale = 0.2; maxYScale = 0.3; xMultiplier = 10. }

          { color = "#fa0" 
            sample = 0.; sampleOffset = 4000.; sampleInterval = 0.01 
            minYScale = 0.3; maxYScale = 0.4; xMultiplier = 50. }

          { color = "#0fa" 
            sample = 0.; sampleOffset = 5000.; sampleInterval = 0.05 
            minYScale = 0.4; maxYScale = 0.5; xMultiplier = 50. }

          { color = "#ff0" 
            sample = 0.; sampleOffset = 6000.; sampleInterval = 0.1 
            minYScale = 0.5; maxYScale = 0.6; xMultiplier = 1. }

          { color = "#faf" 
            sample = 0.; sampleOffset = 6000.; sampleInterval = 0.1 
            minYScale = 0.6; maxYScale = 0.7; xMultiplier = 20. }
      ]

  let drawNoisePlotters p =
      let plot = plotSimpleNoise p

      noiseSimplePlotters <-
          noiseSimplePlotters
          |> List.map plot

  let draw (p:P5) =
      drawNoisePlotters p