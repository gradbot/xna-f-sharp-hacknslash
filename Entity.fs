open System
open Microsoft.Xna.Framework

open Physical
open Gear
open Stats
                        
type Alignment =
    | Good
    | Evil

type Entity(physical : Physical, gear : Gear, stats : Stats, alignment : Alignment) =
    member this.Render(context) =
        physical.Render(context)

    member this.Update(gameTime) =
        physical.Update(gameTime)
        
    member this.Physical
        with get() = physical
        
    member this.Stats
        with get() = stats
        
    member this.Attack(entity : Entity) =
        ()
        
    member this.Distance(entity : Entity) =
        Vector2.Distance(physical.Position, entity.Physical.Position)
        
    member this.CompareDistance (f : float32 -> float32 -> bool) (entity : Entity) x =
        fun () -> f (this.Distance(entity)) x
        
   