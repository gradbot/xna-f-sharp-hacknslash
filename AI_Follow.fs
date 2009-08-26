open Microsoft.Xna.Framework

open Entity
open Physical
open Stats
open Tree
open AI

type Aggression =
    | Static of float
    | Linear
    | Inverted
    | Squared of float
    | InvertedSquared of float

type AI_Follow(entity : Entity, ?_target : Entity, ?_aggression : Aggression) =
    let mutable aggression = if _aggression.IsSome then _aggression.Value else Linear
    let mutable target = if _target.IsSome then _target else None

    let mutable distance = 0.0

    member this.Target 
        with get() = target 
        and set t = target <- Some(t)
    
    interface AI with
        member this.Update(gameTime) =
            if Option.isSome target then
                let dest = target.Value.Physical.Position
                let from = entity.Physical.Position
                distance <- float <| Vector2.Distance(dest, from)
                entity.Physical.Velocity <- Vector2.Normalize(dest - from) * entity.Stats.MaxVelocity
        member this.Priority() =
            if Option.isNone target then
                0.0
            else
                match aggression with
                    | Static(x) -> x
                    | Linear -> distance
                    | Inverted -> if distance = 0.0 then System.Double.MaxValue else 1.0 / distance
                    | Squared(x) -> if distance > x * x then x * x else distance * distance
                    | InvertedSquared(x) -> if distance < x * x then x else x * x / sqrt(distance)
                
        member this.Start() =
            target <- _target
        member this.Stop() =
            target <- None
        member this.Reset() =
            target <- _target

