open Controller

type Player(num : int, controller : Controller) =
    static let mutable count = 0

    static member Create() =
        let controller = Controller.Get()
        match controller with
        | Some(controller) -> 
            count <- count + 1
            Some(Player(count, controller))
        | None -> None
        
    member this.Number =
        num