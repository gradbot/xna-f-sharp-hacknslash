
type Stats(_healthPoints : int, level : int, maxVelocity : float32) =
    let mutable healthPoints = _healthPoints
    
    member this.MaxVelocity = maxVelocity
    
    member this.Alive 
        with get() = healthPoints > 0
        
    static member Default =
        Stats(100, 1, 50.0f)
        
    static member Double =
        Stats(100, 1, 100.0f)

