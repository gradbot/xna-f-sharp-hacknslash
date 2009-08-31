open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
 
type Mouse() =
    static let mutable mouseState = Unchecked.defaultof<MouseState>
    
    static member Update() =
        mouseState <- Mouse.GetState();
        
    static member Position
        with get() = Vector2(float32 mouseState.X, float32 mouseState.Y)