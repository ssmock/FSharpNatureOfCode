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

type CanvasDimension = int
type ColorValue = int

type PositionX = float
type PositionY = float
type PositionZ = float

type SourcePositionX = float
type SourcePositionY = float
type TargetPositionX = float
type TargetPositionY = float

type CenterX = float
type CenterY = float

type Width = float
type Height = float

type P5 =
  // Essentials
  abstract member createCanvas: CanvasDimension -> CanvasDimension -> unit
  abstract member background: U2<string,ColorValue> -> unit
  abstract member fill: U2<string,ColorValue> -> unit
  abstract member stroke: U2<string,ColorValue> -> unit
  abstract member noStroke: unit -> unit
  abstract member noFill: unit -> unit

  // Transformations
  abstract member translate: PositionX -> PositionY -> unit

  // Shapes
  abstract member point: PositionX -> PositionY -> unit
  abstract member point3D: PositionX -> PositionY -> PositionZ -> unit
  abstract member rect: PositionX -> PositionY -> Width -> Height -> unit
  abstract member ellipse: CenterX -> CenterY -> Width -> Height -> unit
  abstract member line: SourcePositionX -> SourcePositionY -> TargetPositionX -> TargetPositionY -> unit

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
  abstract member width: Width
  abstract member height: Height
  abstract member mouseIsPressed: bool
  abstract member mouseX: PositionX
  abstract member mouseY: PositionY