namespace Brains

open Microsoft.Xna.Framework

type AI_Attack(entity : Entity, ?_target : Entity) =
    inherit AI()

    let mutable target = if _target.IsSome then _target else None
    let mutable distance = 0.0

    member this.Target 
        with get() = target 
        and set t = target <- Some(t)
    
    override this.Update(gameTime) =
        if Option.isSome target && this.Running then
            entity.Attack(target.Value)

    override this.Priority() =
        if Option.isNone target then
            0.0
        else
            let dest = target.Value.Physical.Position
            let from = entity.Physical.Position
            distance <- float <| Vector2.Distance(dest, from)
            1.0 / (distance + 0.01)
            

