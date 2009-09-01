//
open AI

type AI_Predicate(aiList : list<(unit -> bool) * AI>) =
    inherit AI()
    
    override this.Priority() =
        1.0
        
    override this.Update(gameTime) =
        aiList
        |> List.iter (fun (condition, ai) -> 
            if condition() then ai.Update(gameTime))

