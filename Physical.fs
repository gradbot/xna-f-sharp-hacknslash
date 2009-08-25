﻿open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Physical(_position : Vector2, _scale : float32, _texture : Texture2D) =
    let mutable position = _position
    let mutable velocity = Vector2()
    let mutable scale = _scale
    let mutable color = Color.White
    let mutable texture = _texture
    
    member this.Render(context : SpriteBatch) =
        let rect = Nullable<Rectangle>()
        let offset = Vector2(float32 texture.Width, float32 texture.Height) * 0.5f
        context.Draw(texture, position - offset, rect, color, 0.0f, Vector2(0.0f), scale, SpriteEffects.None, 0.0f)

    member this.Update(gameTime : float32) =
        position <- position + velocity * gameTime
        
    member this.Velocity 
        with get() = velocity 
        and set x = velocity <- x
        
    member this.Position
        with get() = position