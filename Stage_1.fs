// it may help to have meaningfull names for the stages at some point

namespace Brains

open Microsoft.Xna.Framework

open Mouse

type Stage_1(resource) =
    inherit Stage(resource)
    
    let circle = resource.Texture2D.["circle"]
        
    do  let obj1 = Entity(Physical(Vector2(), 1.0f, circle), Gear(), Stats.Default, Good)
        let obj2 = Entity(Physical(Vector2(), 0.5f, circle), Gear(), Stats.Double, Evil)
        base.Entities <- [obj1; obj2]
        
        let ai1 = 
            AI_Priority(
                [
                    (AI_Follow(obj1, obj2, Static(0.01)) :> AI);
                    (AI_Attack(obj1, obj2) :> AI);
                ])
        
        let ai2 =
            AI_Priority(
                [
                    (AI_MoveTo(obj2, (fun this -> Mouse.LeftButton.Add(this.setDestination))) :> AI);
                ])
        
        base.Ais <- 
            [
            (ai1 :> AI);
            (ai2 :> AI);
            ]
        

