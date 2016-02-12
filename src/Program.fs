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
        Axiom = str2symlist "FFPF"
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

let system2 = {
    System = {
        Axiom = str2symlist "DD"
        Rules = [
                    Symbol 'F', str2symlist "[F[]FV[]]Q"
                    Symbol 'V', str2symlist "Q"
                    Symbol 'Q', str2symlist "+FFV-F-"
                    Symbol 'D', str2symlist "FVF-"
                ]
                |> Map.ofList }

    Translation =
      [ Symbol 'F', [ Forward 1. ]
        Symbol 'V', [ Forward 1. ]
        Symbol 'Q', [ Forward 1. ]
        Symbol 'D', [ Forward 1. ]
        Symbol '+', [ Left 60. ]
        Symbol '-', [ Right 60. ]
        Symbol '[', [ Push ]
        Symbol ']', [ Pop ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    FileName = "system2.html" }

let system3 = {
    System = {
        Axiom = str2symlist "A"
        Rules = [
                    Symbol 'A', str2symlist "B[-ABA]+B"
                    Symbol 'B', str2symlist "A"
                ]
                |> Map.ofList }

    Translation =
      [ Symbol 'A', [ Forward 1. ]
        Symbol 'B', [ Forward 1. ]
        Symbol '+', [ Left 30. ]
        Symbol '-', [ Right 30. ]
        Symbol '[', [ Push ]
        Symbol ']', [ Pop ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    FileName = "system3.html" }

let system4 = {
    System = {
        Axiom = str2symlist "A"
        Rules = [
                    Symbol 'A', str2symlist "-A-B-CBA+++"
                    Symbol 'B', str2symlist "[ACA]"
                    Symbol 'C', str2symlist "B"
                ]
                |> Map.ofList }

    Translation =
      [ Symbol 'A', [ Forward 1.5 ]
        Symbol 'B', [ Forward 2.5 ]
        Symbol 'C', [ Forward 3.5 ]
        Symbol '+', [ Left 15. ]
        Symbol '-', [ Right 15. ]
        Symbol '[', [ Push ]
        Symbol ']', [ Pop ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 60. }}

    FileName = "system4.html" }

let system5 = {
    System = {
        Axiom = str2symlist "A"
        Rules = [
                    Symbol 'A', str2symlist "B"
                    Symbol 'B', str2symlist "[+C]-[+A]-[+C]-"
                    Symbol 'C', str2symlist "+ABA-"
                ]
                |> Map.ofList }

    Translation =
      [ Symbol 'A', [ Forward 1. ]
        Symbol 'B', [ Forward 2. ]
        Symbol 'C', [ Forward 3. ]
        Symbol '+', [ Left 15. ]
        Symbol '-', [ Right 15. ]
        Symbol '[', [ Push ]
        Symbol ']', [ Pop ] ]
      |> Map.ofList

    Start = { Pos = { X = 0.; Y = 0. }; Dir = { Length = Len 0.; Angle = Deg 0. }}

    FileName = "system5.html" }

[<EntryPoint>]
let main argv =
    pythagoras.generateAndShow 7
    sierpinski.generateAndShow 9
    koch.generateAndShow 5
    system1.generateAndShow 4
    system2.generateAndShow 6
    system3.generateAndShow 7
    system4.generateAndShow 6
    system5.generateAndShow 15
    0
