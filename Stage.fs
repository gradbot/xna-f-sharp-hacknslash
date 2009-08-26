open Microsoft.Xna.Framework

open Frame
open Resource
open Entity
open Physical
open Gear
open Stats
open AI

type Stage(resource) =
    let hud = resource.SpriteBatch.["hud"]

    let mutable entities = List<Entity>.Empty
    let mutable ais = List<AI>.Empty
    
    member this.Entities 
        with get() = entities    
        and set (x) = entities <- x

    member this.Ais 
        with get() = ais    
        and set (x) = ais <- x
    
    interface Frame with
        member this.Draw(gameTime) =
            hud.Begin();
            entities |> List.iter (fun o -> o.Render(hud))
            hud.End();
            
        member this.Update(gameTime) =
            entities |> List.iter (fun x -> x.Update(gameTime))
            ais |> List.iter (fun x -> x.Update(gameTime))
            
