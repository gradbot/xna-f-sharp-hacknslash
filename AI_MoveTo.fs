namespace Brains

open Microsoft.Xna.Framework

type AI_MoveTo(entity : Entity, bind : AI_MoveTo -> unit) as self =
    inherit AI()
    
    let mutable destination = Vector2()
    
    do bind self

    member this.Destination
        with get() = destination 
        and set x = destination <- x
        
    member this.setDestination x =
        destination <- x
        this.Start()
    
    override this.Update(gameTime) =
        if this.Running then
            let distance = Vector2.Distance(destination, entity.Physical.Position)
            let direction = (destination - entity.Physical.Position) / distance
            
            if entity.Stats.MaxVelocity * gameTime > distance then
                entity.Physical.Position <- destination
                entity.Physical.Velocity <- Vector2()//direction * distance / gameTime
                base.Stop()
            else
                entity.Physical.Velocity <- direction * entity.Stats.MaxVelocity
            
    override this.Priority() =
        if this.Running then
            float <| Vector2.Distance(destination, entity.Physical.Position)
        else
            0.0
            
