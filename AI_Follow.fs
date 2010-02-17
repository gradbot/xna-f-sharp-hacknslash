namespace Brains

open Microsoft.Xna.Framework

type Aggression =
    | Static of float
    | Linear
    | Inverted
    | Squared of float
    | InvertedSquared of float

type AI_Follow(entity : Entity, ?_target : Entity, ?_aggression : Aggression) =
    inherit AI()
    
    let mutable aggression = if _aggression.IsSome then _aggression.Value else Linear
    let mutable target = if _target.IsSome then _target else None

    let mutable distance = 0.0
    let mutable follow = false

    member this.Target 
        with get() = target 
        and set t = target <- Some(t)
    
    override this.Update(gameTime) =
        if Option.isSome target && this.Running then
            let dest = target.Value.Physical.Position
            let from = entity.Physical.Position
            distance <- float <| Vector2.Distance(dest, from)
            entity.Physical.Velocity <- Vector2.Normalize(dest - from) * entity.Stats.MaxVelocity
            
    override this.Priority() =
        if Option.isNone target then
            0.0
        else
            match aggression with
                | Static(x) -> x
                | Linear -> distance
                | Inverted -> if distance = 0.0 then System.Double.MaxValue else 1.0 / distance
                | Squared(x) -> if distance > x * x then x * x else distance * distance
                | InvertedSquared(x) -> if distance < x * x then x else x * x / sqrt(distance)
            
