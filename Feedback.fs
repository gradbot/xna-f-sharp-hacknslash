
open Microsoft.Xna.Framework

open Entity
open Physical
open Stats
open Tree

type FeedBack =
    abstract member Rate : unit -> float
    

type FeedBack_Distance(_entities : Entity list) =
    let mutable entities = _entities
    
    
    
