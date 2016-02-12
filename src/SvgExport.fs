namespace LSystems
open LSystems.Turtle

module SvgExport =

    let viewportWidth = 1000.

    let viewportHeight = 1000.

    let header = """
<!DOCTYPE html>
<html>
<head>
<style>
svg path { stroke-width:1; fill: transparent; }
</style>
</head>
<body>
<div style="text-align: center; width: 100%">
<svg height="50%" width="50%" viewbox="0 0 1000 1000" preserveAspectRatio="xMidYMid meet" style="margin: 20px">
<g id="drawing">
"""

    let footer = """
</g>
</svg>
</div>
</body>
</html>
"""
    
    let toSvg scaleX scaleY (ops : Operations seq) =
        let rand = new System.Random()
        let r = rand.Next(0, 64)
        let g = rand.Next(0, 96)
        let b = rand.Next(0, 128)
        let asString color op =
            match op with
            | Draw (p1, p2) ->
              sprintf """<path d="M%f %f L%f %f" style="stroke:rgb(%i,%i,%i)" />""" (scaleX p1.X) (scaleY p1.Y) (scaleX p2.X) (scaleY p2.Y) ((color + r) % 256) ((color + g) % 256) ((color + b) % 256)
        [ yield header
          for i, op in Seq.indexed ops -> asString i op
          yield footer ]
        |> String.concat "\n"

    let calculateWindow (operations : Operations seq) =
        let minOf = function Draw (p1, p2) -> min p1.X p2.X, min p1.Y p2.Y
        let maxOf = function Draw (p1, p2) -> max p1.X p2.X, max p1.Y p2.Y
        let mins =
            operations
            |> Seq.map (fun op -> minOf op)
            |> Seq.reduce (fun (x1, y1) (x2, y2) -> min x1 x2, min y1 y2)
        let maxs =
            operations
            |> Seq.map (fun op -> maxOf op)
            |> Seq.reduce (fun (x1, y1) (x2, y2) -> max x1 x2, max y1 y2)
        mins, maxs

    let calculateScale (operations : Operations seq) =
        let actualWindow r =
            if r > 1.
            then viewportWidth, viewportHeight / r
            else viewportWidth * r, viewportHeight

        let (minx, miny), (maxx, maxy) = calculateWindow operations
        let w = abs (maxx - minx)
        let h = abs (maxy - miny)
        let r = if w = 0. || h = 0. then 1. else w / h
        let ww, wh = actualWindow r
        let sx = if w = 0. then 1. else ww / w
        let sy = if h = 0. then 1. else wh / h
        let funX x = (x - minx) * sx
        let funY y = (y - miny) * sy
        funX, funY

    let createSvg path operations =
        let scaleX, scaleY = calculateScale operations
        System.IO.File.WriteAllText(path, toSvg scaleX scaleY operations)
