namespace LSystems
open LSystems.Turtle

module SvgExport =

    let viewportWidth = 1000.

    let viewportHeight = 1000.

    let header = """
<!DOCTYPE html>
<html>
<head>
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
        let asString = function
            | Draw (p1, p2) ->
              sprintf """<line x1="%f" y1="%f" x2="%f" y2="%f" style="stroke:rgb(0,0,192);stroke-width:1" />""" (scaleX p1.X) (scaleY p1.Y) (scaleX p2.X) (scaleY p2.Y)
        [ yield header
          for op in ops -> asString op
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
