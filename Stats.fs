
type Stats(_healthPoints : int, level : int) =
    let mutable healthPoints = _healthPoints
    
    member this.MaxVelocity = 50.0f
    
    member this.Alive 
        with get() = healthPoints > 0
        
    static member Default =
        Stats(100, 1)