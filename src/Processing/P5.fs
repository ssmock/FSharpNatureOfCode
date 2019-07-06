namespace Processing

open Fable.Core

module P5 =
  [<Emit("
  new p5(function (p) {
    p.setup = () => setup(p);
    p.draw = () => draw(p);
  });
  ")>]
  let StartProcessing () = jsNative
  
  type ColorArgs = {
    R: int
    G: int
    B: int
    A: float
  }

  let Rgba (c:ColorArgs) =
    sprintf "rgba(%d,%d,%d,%f)" c.R c.G c.B c.A

type P5 =
  // Essentials
  abstract member createCanvas: int -> int -> unit
  abstract member background: U2<string,int> -> unit
  abstract member fill: U2<string,int> -> unit
  abstract member stroke: U2<string,int> -> unit
  abstract member noStroke: unit -> unit
  abstract member noFill: unit -> unit

  // Shapes
  abstract member point: float -> float -> unit
  abstract member point3D: float -> float -> float -> unit
  abstract member rect: float -> float -> float -> float -> unit
  abstract member ellipse: float -> float -> float -> float -> unit
  
  // RNG
  abstract member random: float -> float
  abstract member randomFromRange: float -> float -> float
  abstract member randomPick: 'a array -> 'a 
  abstract member randomGaussian: float -> float -> float
  abstract member noiseSeed: float -> unit
  abstract member noiseDetail: float -> float -> unit

  abstract member noise: float -> float
  abstract member noise2: float -> float -> float
  abstract member noise3: float -> float -> float -> float

  // Math
  abstract member map: float -> float -> float -> float -> float -> float
  abstract member mapClamp: float -> float -> float -> float -> float -> float
  abstract member lerp: float -> float -> float -> float

  // State
  abstract member width: float
  abstract member height: float
  abstract member mouseIsPressed: bool
  abstract member mouseX: float
  abstract member mouseY: float