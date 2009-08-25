
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Audio

type XNAEngine() =
    let mutable graphics = Unchecked.defaultof<GraphicsDeviceManager>
    let mutable content = Unchecked.defaultof<ContentManager>
    let mutable audioEngine = Unchecked.defaultof<AudioEngine>

    member this.Init(game : Game) =
        game.Window.Title <- "Brains - a Zombie RPG"

        graphics <- new GraphicsDeviceManager(game)
        graphics.PreferredBackBufferWidth <- 600
        graphics.PreferredBackBufferHeight <- 800
        graphics.PreferMultiSampling <- true
        graphics.SynchronizeWithVerticalRetrace <- true
        content <- new ContentManager(game.Services)
        
    member this.gd =
        graphics.GraphicsDevice


