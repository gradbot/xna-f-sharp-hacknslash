// This AI has a list of other AIs and will run whichever has the highest value.
open Microsoft.Xna.Framework

open Entity
open Physical
open Stats
open AI
open Tree

type AI_Weighted(entity : Entity, weight : float) =
    inherit AI()
    
    let mutable running : AI Option = None
    let mutable recalculate = false

    member this.Weight =
        weight
    
    member this.heavyist() =
        this.Siblings |> Seq.maxBy (fun ai -> ai.Weight)

    member this.Runner
        with get() = running
        and set(x) = 
            if Option.isSome running then
                running.Value.Stop()
            running <- Some(x)

    override this.Update(gameTime) =
        match running, recalculate with
        | Some(ai), true  -> 
            let heavyist = this.heavyist()
            if ai <> heavyist then
                this.Runner <- heavyist
        | None, _ -> this.Runner <- this.heavyist()
        | _, _ -> ()

