
type AI =
    abstract Start : unit -> unit
    abstract Stop : unit -> unit
    abstract Update : float32 -> unit
    abstract Priority : unit -> float
