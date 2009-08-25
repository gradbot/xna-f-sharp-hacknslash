open Microsoft.Xna.Framework

open Entity
open Physical
open Stats
open Tree

[<AbstractClass>]
type AI() =
    inherit Tree<AI>
    
    abstract Start : unit -> unit
    abstract Stop : unit -> unit
    abstract Update : float -> unit
