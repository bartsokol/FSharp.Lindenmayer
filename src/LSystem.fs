namespace LSystems

module LSystem =

    type Symbol = Symbol of char

    type State = Symbol list

    type Rules = Map<Symbol, State>

    type LSystem = {
        Axiom : State
        Rules : Rules }

    let applyRules (rules : Rules) (sym : Symbol) =
        match rules.TryFind(sym) with
        | None -> [sym]
        | Some x -> x

    let evolve (rules : Rules) (state : State) =
        [ for sym in state do yield! (applyRules rules sym) ]

    let forward (sys : LSystem) =
        let initial = sys.Axiom
        let generate = evolve sys.Rules
        initial |> Seq.unfold (fun state -> Some(state, generate state))

    let generation gen system =
        system
        |> forward
        |> Seq.item gen
        |> Seq.toList

    let str2symlist text =
        text
        |> Seq.map (fun c -> Symbol c)
        |> List.ofSeq
