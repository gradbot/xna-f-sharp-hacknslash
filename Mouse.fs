module Mouse

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

// compiler no longer allows this to be a static member as of 1.9.9.9
let mutable mouseState = Unchecked.defaultof<MouseState>
 
type Mouse() =
    static let mutable first = true

    static let move = new Event<_>()
    static let leftButton = new Event<_>()
    static let rightButton = new Event<_>()
    static let middleButton = new Event<_>()
    static let scrollWheelValue = new Event<_>()

    static member Position
        with get() = Vector2(float32 mouseState.X, float32 mouseState.Y)
                
    static member Update() =
        let newState = Mouse.GetState()
        
        if first then
            mouseState <- newState
            first <- false
        
        if newState.X <> mouseState.X || newState.Y <> mouseState.Y then
            move.Trigger(Mouse.Position)
        
        if newState.LeftButton = ButtonState.Pressed && mouseState.LeftButton = ButtonState.Released then
            leftButton.Trigger(Mouse.Position)
        
        if newState.RightButton = ButtonState.Pressed && mouseState.RightButton = ButtonState.Released then
            rightButton.Trigger(Mouse.Position)
        
        if newState.MiddleButton = ButtonState.Pressed && mouseState.MiddleButton = ButtonState.Released then
            middleButton.Trigger(Mouse.Position)
        
        if newState.ScrollWheelValue <> mouseState.ScrollWheelValue then
            scrollWheelValue.Trigger(mouseState.ScrollWheelValue)
            
        mouseState <- newState
        
    static member Move
        with get() = move.Publish
        
    static member LeftButton
        with get() = leftButton.Publish

    static member RightButton
        with get() = rightButton.Publish

    static member MiddleButton
        with get() = middleButton.Publish

    static member ScrollWheelValue
        with get() = scrollWheelValue.Publish

