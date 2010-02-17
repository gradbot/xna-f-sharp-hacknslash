module Controller

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

type Controller(id : int) =
    static let mutable count = 0
    static let state = 
        [|
            Option<GamePadState>.None; 
            Option<GamePadState>.None; 
            Option<GamePadState>.None; 
            Option<GamePadState>.None|]
    // this is just a moc that allows 4 players
    static member Get() =
        if count < 4 then
            count <- count + 1
            Some(Controller(count))
        else
            None
    
    static member Update() =
        state.[0] <- Some(GamePad.GetState(PlayerIndex.One))
        