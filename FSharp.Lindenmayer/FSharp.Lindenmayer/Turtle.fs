namespace LSystems
open LSystems.LSystem

module Turtle =
    
    type Length = Len of float

    type Angle = Deg of float

    let angleVal = function
        Deg x -> x

    let add (a1 : Angle) (a2 : Angle) =
        Deg ((angleVal a1) + (angleVal a2))

    type Instructions =
        | Move of Length
        | Turn of Angle
        | Push
        | Pop

    let Forward x = Move (Len x)
    let Left x = Turn (Deg x)
    let Right x = Turn (Deg -x)

    type Position = { X : float; Y : float }

    type Direction = { Length : Length; Angle : Angle }

    type Turtle = { Pos : Position; Dir : Direction }

    type ProgState = { Current : Turtle; Stack : Turtle list }

    let turn angle turtle =
        let newAngle = turtle.Dir.Angle |> add angle
        { turtle with Dir = { turtle.Dir with Angle = newAngle }}

    type Translation = Map<Symbol, Instructions list>

    type Operations = Draw of Position * Position

    let pi = System.Math.PI

    let line (pos : Position) (len : Length) (ang : Angle) =
        let l = match len with Len l -> l
        let a = match ang with Deg a -> (a * pi / 180.)
        { X = pos.X + l * cos a; Y = pos.Y + l * sin a }

    let execute (inst : Instructions) (state : ProgState) =
        match inst with
        | Push -> None, { state with Stack = state.Current :: state.Stack }
        | Pop ->
            match state.Stack with
            | [] -> None, state
            | head::tail -> None, { state with Current = head; Stack = tail }
        | Turn angle -> None, { state with Current = state.Current |> turn angle }
        | Move len ->
            let startPoint = state.Current.Pos
            let endPoint = line startPoint len state.Current.Dir.Angle
            Some (Draw (startPoint, endPoint)), { state with Current = { state.Current with Pos = endPoint }}

    let toTurtle (start : Turtle) (trans : Translation) (symbols :  Symbol list) =
        let initial = {
            Current = { Pos = start.Pos; Dir = start.Dir }
            Stack = [] }
        symbols
        |> List.map (fun sym -> trans.[sym])
        |> List.concat
        |> Seq.scan (fun (_, state) inst -> execute inst state) (None, initial)
        |> Seq.map fst
        |> Seq.choose id
