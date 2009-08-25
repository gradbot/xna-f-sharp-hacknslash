open Microsoft.Xna.Framework

open Frame
open Resource
open Entity
open Physical
open Gear
open Stats
open AI

type Stage(resource) =
    let circle = resource.Texture2D.["circle"]
    let hud = resource.SpriteBatch.["hud"]

    let mutable entities = []
    let mutable ais = List<AI>.Empty
    
    do  let obj1 = Entity(Physical(Vector2(), 1.0f, circle), Gear(), Stats.Default, Good)
        let obj2 = Entity(Physical(Vector2(200.0f, 200.0f), 0.5f, circle), Gear(), Stats.Default, Evil)
        entities <- [obj1; obj2]
        let ai1 = AI(obj1)
        ai1.Target <- obj2
        ais <- [ai1]
        
    
    interface Frame with
        member this.Draw(gameTime) =
            hud.Begin();
            entities |> List.iter (fun o -> o.Render(hud))
            hud.End();
            
        member this.Update(gameTime) =
            entities |> List.iter (fun x -> x.Update(gameTime))
            ais |> List.iter (fun x -> x.Update(gameTime))
            
