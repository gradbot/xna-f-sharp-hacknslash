open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input
 
type Mouse() =
    static let mutable mouseState = Unchecked.defaultof<MouseState>
    static let mutable first = true

    static let moveTrigger, move = Event.create()
    static let leftButtonTrigger, leftButton = Event.create()
    static let rightButtonTrigger, rightButton = Event.create()
    static let middleButtonTrigger, middleButton = Event.create()
    static let scrollWheelValueTrigger, scrollWheelValue = Event.create()

    static member Position
        with get() = Vector2(float32 mouseState.X, float32 mouseState.Y)
                
    static member Update() =
        let newState = Mouse.GetState()
        
        if first then
            mouseState <- newState
            first <- false
        
        if newState.X <> mouseState.X || newState.Y <> mouseState.Y then
            moveTrigger(Mouse.Position)
        
        if newState.LeftButton = ButtonState.Pressed && mouseState.LeftButton = ButtonState.Released then
            leftButtonTrigger(Mouse.Position)
        
        if newState.RightButton = ButtonState.Pressed && mouseState.RightButton = ButtonState.Released then
            rightButtonTrigger(Mouse.Position)
        
        if newState.MiddleButton = ButtonState.Pressed && mouseState.MiddleButton = ButtonState.Released then
            middleButtonTrigger(Mouse.Position)
        
        if newState.ScrollWheelValue <> mouseState.ScrollWheelValue then
            scrollWheelValueTrigger(mouseState.ScrollWheelValue)
            
        mouseState <- newState
        
    static member Move
        with get() = move
        
    static member LeftButton
        with get() = leftButton

    static member RightButton
        with get() = rightButton

    static member MiddleButton
        with get() = middleButton

    static member ScrollWheelValue
        with get() = scrollWheelValue

