open Microsoft.Xna.Framework

open Entity
open Physical
open Stats

type AI(entity : Entity) =
    let mutable target : Entity Option = None

    member this.Target 
        with get() = target 
        and set t = target <- Some(t)

    member this.Update(gameTime) =
        //gameTime, physical, stats
        if Option.isSome this.Target then
            this.attack()

    member this.attack() =
        let dest = target.Value.Physical.Position
        let from = entity.Physical.Position
        
        entity.Physical.Velocity <- Vector2.Normalize(dest - from) * entity.Stats.MaxVelocity

