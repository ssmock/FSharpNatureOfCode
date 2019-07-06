To run:

1. npm install
2. npm start

### How it works

Like Processing, P5 aspires to simplicity, and especially a low barrier to
entry. To achieve this, its default behavior is to rely on conventions,
and globally-available objects. This makes integrating with tools like Webpack,
TypeScript, and Fable a little bit tricky.

Fortunately, P5 also offers [instance mode](https://github.com/processing/p5.js/wiki/Global-and-instance-mode).
This lets us bootstrap a sketch as part of normal initialization, like this:

```
new p5(function (p) {
  p.setup = () => { /* Um... */ };
  p.draw = () => { /* Um... */ };
});
```

This bit of JS is easy enough to `Emit`, but what is the best way to implement
`setup` and `draw` using F#? Turns out, all we need to do is ensure that
a couple of F# functions with the right signatures are in scope where the
bootstrap script is emitted. With those in place, our bootstrap script becomes:

```
new p5(function (p) {
  p.setup = () => setup(p);
  p.draw = () => draw(p)
});
```

### This was originally a clone of the Fable 2.1 Minimal App

Source here: https://github.com/fable-compiler/fable2-samples