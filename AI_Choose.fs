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
        | Closest(x) -> aiListWeight |> List.minBy (fun (ai, priority) -> abs(x - priority))
        | Min -> aiListWeight |> List.minBy snd
        | Max -> aiListWeight |> List.maxBy snd
        | Average -> 
            let average = aiListWeight |> List.averageBy snd
            aiListWeight |> List.minBy snd

type AI_Choose =
    | AI of list<AI> * Condition
    | AI_Weighted of list<AI * float> * Condition
    
    member this.Active() =
        match this with
        | AI(aiList, condition) -> 
            aiList 
            |> List.map (fun ai -> ai, ai.Priority())
            |> condition.Select
            |> fst
        | AI_Weighted(aiList, condition) -> 
            aiList 
            |> List.map (fun (ai, weight) -> ai, weight * ai.Priority()) 
            |> condition.Select
            |> fst
    
    interface AI with
        member this.Start() =
            ()
        member this.Stop() =
            ()
        member this.Priority() =
            this.Active().Priority()
            //| AI_Weighted_Min(x) -> x |> List.minBy (fun (ai, weight) -> ai.Priority() * weight)
        member this.Update(gameTime) =
            ()
