
[<AbstractClass>]
type AI() =
    let mutable running = false
    
    member this.Running
        with get() = running
    
    abstract Start : unit -> unit
    default this.Start() =
        running <- true
        
    abstract Stop : unit -> unit
    default this.Stop() =
        running <- false

    abstract Reset : unit -> unit
    default this.Reset() =
        running <- true

    abstract Update : float32 -> unit
    abstract Priority : unit -> float
