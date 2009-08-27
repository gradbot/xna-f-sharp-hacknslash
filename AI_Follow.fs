open Microsoft.Xna.Framework

open Entity
open Physical
open Stats
open Tree
open AI

type AI_Follow(entity : Entity, ?_target : Entity) =
    let mutable target = Option<Entity>.None

    member this.Target 
        with get() = target 
        and set t = target <- Some(t)
    
    interface AI with
        member this.Update(gameTime) =
            if Option.isSome target then
                let dest = target.Value.Physical.Position
                let from = entity.Physical.Position
                entity.Physical.Velocity <- Vector2.Normalize(dest - from) * entity.Stats.MaxVelocity
        member this.Priority() =
            1.0
        member this.Start() =
            target <- _target
        member this.Stop() =
            target <- None

