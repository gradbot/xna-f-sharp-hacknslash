open Microsoft.Xna.Framework

open Entity
open Physical
open Stats
open Tree
open AI

type AI_Attack(entity : Entity, ?_target : Entity) =
    let mutable target = if _target.IsSome then _target else None
    let mutable distance = 0.0

    member this.Target 
        with get() = target 
        and set t = target <- Some(t)
    
    interface AI with
        member this.Update(gameTime) =
            if Option.isSome target then
                entity.Attack(target.Value)
        member this.Priority() =
            if Option.isNone target then
                0.0
            else
                let dest = target.Value.Physical.Position
                let from = entity.Physical.Position
                distance <- float <| Vector2.Distance(dest, from)
                1.0 / (distance + 0.01)
                
        member this.Start() =
            target <- _target
        member this.Stop() =
            target <- None
        member this.Reset() =
            target <- _target


