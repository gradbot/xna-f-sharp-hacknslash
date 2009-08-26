// This AI will handle group movement and action such as 
// flocking together, attacking a single target, and alerting fellow AI

open AI

type Condition =
    | Closest of float
    | Min
    | Max
    | Average
    member this.Select (aiListWeight : list<AI * float>) =
        match this with
        | Closest(x) -> 
            aiListWeight 
            |> List.minBy (fun (ai, priority) -> abs(x - priority))
        | Min -> aiListWeight |> List.minBy snd
        | Max -> aiListWeight |> List.maxBy snd
        | Average -> 
            let average = aiListWeight |> List.averageBy snd
            aiListWeight
            |> List.minBy (fun (ai, priority) -> abs(average - priority))

[<AbstractClass>]
type AI_Choose() =
    let mutable chosen = Option<AI>.None
    
    abstract member Choose : unit -> AI

    member this.Chosen
        with get() = 
            if Option.isNone chosen then
                chosen <- Some(this.Choose())
            chosen.Value
        and set(x) =
            if Option.isSome chosen then
                chosen.Value.Stop()
            chosen <- Some(x)
            x.Start()
        
    interface AI with
        member this.Start() =
            this.Chosen.Start()
        member this.Stop() =
            this.Chosen.Stop()
        member this.Reset() =
            this.Chosen <- this.Choose()
        member this.Priority() =
            this.Chosen.Priority()
        member this.Update(gameTime) =
            this.Chosen <- this.Choose()
            this.Chosen.Update(gameTime)
            
type AI_Priority(aiList : list<AI>, ?_condition : Condition) =
    inherit AI_Choose()
    let mutable condition = if _condition.IsSome then _condition.Value else Max

    override this.Choose() =
        aiList 
        |> List.map (fun ai -> ai, ai.Priority())
        |> condition.Select
        |> fst

type AI_Weighted_Priority(aiList : list<AI * float>, ?_condition : Condition) =
    inherit AI_Choose()
    let mutable condition = if _condition.IsSome then _condition.Value else Max

    override this.Choose() =
        aiList 
        |> List.map (fun (ai, weight) -> ai, weight * ai.Priority())
        |> condition.Select
        |> fst
