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
open AI_Predicate

type Stage_1(resource) =
    inherit Stage(resource)
    
    let circle = resource.Texture2D.["circle"]
    
    let trigger, event = Event.create()
    
    do  let obj1 = Entity(Physical(Vector2(), 1.0f, circle), Gear(), Stats.Default, Good)
        let obj2 = Entity(Physical_Mouse(0.5f, circle), Gear(), Stats.Default, Evil)
        base.Entities <- [obj1; obj2]
        let ai1 = 
            AI_Priority(
                [
                    (AI_Follow(obj1, obj2, Static(0.01)) :> AI);
                    (AI_Attack(obj1, obj2) :> AI);
                ])
        
        // I'm just tossing around some ideas.
        // I don't think I'm going to implement a scripting language for AI
        let ai2 =
            AI_Predicate(
                [
                    (obj1.CompareDistance (<) obj2 0.2f, 
                     (AI_Follow(obj1, obj2, Static(0.001)) :> AI));
                ])
        
        base.Ais <- [(ai1 :> AI)]
        

