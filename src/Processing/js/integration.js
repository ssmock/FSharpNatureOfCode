/**
 * This script extends p5 with aliased function overloads.
 * For how these are used, see P5.fs, and note the variations
 * between, e.g. noise and noise2.
 */

import * as p5 from "p5"

Object.assign(p5.prototype, {
  randomFromRange: p5.prototype.random,

  // Weird: Fable converts arrays to typed arrays when
  // possible; here, we convert it back before passing
  // it to P5.
  randomPick: (arr) => p5.prototype.random([...arr]),

  noise2: p5.prototype.noise,
  noise3: p5.prototype.noise,

  mapClamp: (value, start1, stop1, start2, stop2) => 
    p5.prototype.map(value, start1, stop1, start2, stop2, true),

  point3D: p5.prototype.point
});