// AI classes make decisions based on their child AI priorities and action feedback

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
    inherit AI()
    
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
        
    override this.Start() =
        base.Start()
        this.Chosen.Start()
    override this.Stop() =
        base.Stop()
        this.Chosen.Stop()
    override this.Reset() =
        base.Reset()
        this.Chosen <- this.Choose()
    override this.Priority() =
        this.Chosen.Priority()
    override this.Update(gameTime) =
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
