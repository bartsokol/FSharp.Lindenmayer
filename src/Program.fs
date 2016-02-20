open LSystems.LSystem
open LSystems.Turtle
open LSystems.SvgExport

type SystemDefinition = {
    System : LSystem
    Translation : Map<Symbol, Instructions list>
    Start : Turtle
    FileName : string }

type SystemDefinition with
    member this.generateAndShow gen =
        this.System |> generation gen |> toTurtle this.Start this.Translation |> createSvg this.FileName
        ignore <| System.Diagnostics.Process.Start(this.FileName)

let pythagoras = {    
    System = {
        Axiom = [ Symbol '0' ]
        Rules = [
                    Symbol '1', str2symlist "11"
                    Symbol '0', str2symlist "1[+0]-0"
                ]
                |> Map.ofList }

    Translation =
      [ Symbol '0', [ Forward 1. ]
        Symbol '1', [ Forward 1. ]
        Symbol '+', [ Left 45. ]
        Symbol '-', [ Right 45. ]
        Symbol '[', [ Push ]
        Symbol ']', [ Pop ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg -90. }}

    FileName = "pythagoras.html" }
    
let sierpinski = {
    System = {
        Axiom = [ Symbol 'A' ]
        Rules = [                   
                    Symbol 'A', str2symlist "+B-A-B+"
                    Symbol 'B', str2symlist "-A+B+A-"
                ]
                |> Map.ofList }

    Translation =
      [ Symbol 'A', [ Forward 1. ]
        Symbol 'B', [ Forward 1. ]
        Symbol '+', [ Left 60. ]
        Symbol '-', [ Right 60. ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    FileName = "sierpinski.html" }

let koch = {
    System = {
        Axiom = [ Symbol 'F' ]
        Rules = [
                    Symbol 'F', str2symlist "F+F-F-F+F"
                ]
                |> Map.ofList }

    Translation =
      [ Symbol 'F', [ Forward 1. ]
        Symbol '+', [ Left 90. ]
        Symbol '-', [ Right 90. ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    FileName = "koch.html" }

let system1 = {
    System = {
        Axiom = [ Symbol 'F'; Symbol 'F'; Symbol 'P'; Symbol 'F' ]
        Rules = [
                    Symbol 'F', str2symlist "PF++F[FF-F+PF+FPP][F]FFPF"
                    Symbol 'P', [ ]
                ]
                |> Map.ofList }

    Translation =
      [ Symbol 'F', [ Forward 1. ]
        Symbol 'P', [ Forward 1. ]
        Symbol '+', [ Left 60. ]
        Symbol '-', [ Right 60. ]
        Symbol '[', [ Push ]
        Symbol ']', [ Pop ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    FileName = "system1.html" }

[<EntryPoint>]
let main argv =
    pythagoras.generateAndShow 8
    sierpinski.generateAndShow 8
    koch.generateAndShow 6
    system1.generateAndShow 3
    0
