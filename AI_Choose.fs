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
    
    abstract member Choose : unit -> AI * float

    // getting will not start the AI
    // setting will if none zero priority
    member this.Chosen
        with get() = 
            if Option.isNone chosen then
                chosen <- Some(fst <| this.Choose())
                
            chosen.Value
            
        and set(choice, priority) =
            if Option.isSome chosen && chosen.Value <> choice then
                chosen.Value.Stop()
                
            chosen <- Some(choice)
            
            if priority > 0.0 then
                choice.Start()
        
    override this.Start() =
        base.Start()
//        if Option.isSome chosen then
//            chosen.Value.Start()
        
    override this.Stop() =
        base.Stop()
        if Option.isSome chosen then
            chosen.Value.Stop()
        
    override this.Reset() =
        base.Reset()
        this.Chosen <- this.Choose()
        
    override this.Priority() =
        if Option.isSome chosen then
            chosen.Value.Priority()
        else
            0.0
        
    override this.Update(gameTime) =
        this.Chosen <- this.Choose()
        
        if Option.isSome chosen then
            chosen.Value.Update(gameTime)
            
type AI_Priority(aiList : list<AI>, ?_condition : Condition) =
    inherit AI_Choose()
    let mutable condition = if _condition.IsSome then _condition.Value else Max

    override this.Choose() =
        aiList 
        |> List.map (fun ai -> ai, ai.Priority())
        |> condition.Select

type AI_Weighted_Priority(aiList : list<AI * float>, ?_condition : Condition) =
    inherit AI_Choose()
    let mutable condition = if _condition.IsSome then _condition.Value else Max

    override this.Choose() =
        aiList 
        |> List.map (fun (ai, weight) -> ai, weight * ai.Priority())
        |> condition.Select
