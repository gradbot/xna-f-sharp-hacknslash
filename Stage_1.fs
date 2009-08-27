// it may help to have meaningfull names for the stages at some point

open Microsoft.Xna.Framework

open Frame
open Resource
open Entity
open Physical
open Gear
open Stats
open AI
open AI_Follow
open Stage

type Stage_1(resource) =
    inherit Stage(resource)
    
    let circle = resource.Texture2D.["circle"]
    
    do  let obj1 = Entity(Physical(Vector2(), 1.0f, circle), Gear(), Stats.Default, Good)
        let obj2 = Entity(Physical(Vector2(200.0f, 200.0f), 0.5f, circle), Gear(), Stats.Default, Evil)
        base.Entities <- [obj1; obj2]
        let ai1 = AI_Follow(obj1)
        ai1.Target <- obj2
        base.Ais <- [(ai1 :> AI)]
        
 