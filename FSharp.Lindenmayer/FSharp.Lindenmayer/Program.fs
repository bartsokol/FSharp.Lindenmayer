open LSystems.LSystem
open LSystems.Turtle
open LSystems.SvgExport

module Pythagoras =
    
    let system = {
        Axiom = [ Symbol '0' ]
        Rules = [
                    Symbol '1', [ Symbol '1'; Symbol '1' ]
                    Symbol '0', [ Symbol '1'; Symbol '['; Symbol '0'; Symbol ']'; Symbol '0' ]
                ]
                |> Map.ofList }

    let translation =
      [ Symbol '0', [ Forward 1. ]
        Symbol '1', [ Forward 1. ]
        Symbol '[', [ Push; Left 45. ]
        Symbol ']', [ Pop; Right 45. ] ]
      |> Map.ofList

    let start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg -90. }}

    let fileName = "pythagoras.html"
    
module Sierpinski =

    let system = {
        Axiom = [ Symbol 'A' ]
        Rules = [
                    Symbol 'A', [ Symbol '+'; Symbol 'B'; Symbol '-'; Symbol 'A'; Symbol '-'; Symbol 'B'; Symbol '+' ]
                    Symbol 'B', [ Symbol '-'; Symbol 'A'; Symbol '+'; Symbol 'B'; Symbol '+'; Symbol 'A'; Symbol '-' ]
                ]
                |> Map.ofList }

    let translation =
      [ Symbol 'A', [ Forward 1. ]
        Symbol 'B', [ Forward 1. ]
        Symbol '+', [ Left 60. ]
        Symbol '-', [ Right 60. ] ]
      |> Map.ofList

    let start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    let fileName = "sierpinski.html"

module Koch =

    let system = {
        Axiom = [ Symbol 'F' ]
        Rules = [
                    Symbol 'F', [ Symbol 'F'; Symbol '+'; Symbol 'F'; Symbol '-'; Symbol 'F'; Symbol '-'; Symbol 'F'; Symbol '+'; Symbol 'F' ]
                ]
                |> Map.ofList }

    let translation =
      [ Symbol 'F', [ Forward 1. ]
        Symbol '+', [ Left 90. ]
        Symbol '-', [ Right 90. ] ]
      |> Map.ofList

    let start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    let fileName = "koch.html"

[<EntryPoint>]
let main argv =
    
    Pythagoras.system |> generation 8 |> toTurtle Pythagoras.start Pythagoras.translation |> createSvg Pythagoras.fileName
    ignore <| System.Diagnostics.Process.Start(Pythagoras.fileName)
    
    Sierpinski.system |> generation 8 |> toTurtle Sierpinski.start Sierpinski.translation |> createSvg Sierpinski.fileName
    ignore <| System.Diagnostics.Process.Start(Sierpinski.fileName)
    
    Koch.system |> generation 6 |> toTurtle Koch.start Koch.translation |> createSvg Koch.fileName
    ignore <| System.Diagnostics.Process.Start(Koch.fileName)
    
    0
