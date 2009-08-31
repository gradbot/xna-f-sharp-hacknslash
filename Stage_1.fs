// it may help to have meaningfull names for the stages at some point

open Microsoft.Xna.Framework

open Frame
open Resource
open Entity
open Physical
open Gear
open Stats
open Stage
open AI
open AI_Follow
open AI_Choose
open AI_Attack

type Stage_1(resource) =
    inherit Stage(resource)
    
    let circle = resource.Texture2D.["circle"]
    
    do  let obj1 = Entity(Physical(Vector2(), 1.0f, circle), Gear(), Stats.Default, Good)
        let obj2 = Entity(Physical(Vector2(200.0f, 200.0f), 0.5f, circle), Gear(), Stats.Default, Evil)
        base.Entities <- [obj1; obj2]
        let ai1 = 
            AI_Priority(
                [
                    (AI_Follow(obj1, obj2, Static(0.01)) :> AI);
                    (AI_Attack(obj1, obj2) :> AI);
                ])
        base.Ais <- [(ai1 :> AI)]
        
 