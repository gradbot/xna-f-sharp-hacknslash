open System.Collections.Generic

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Audio

open XNAEngine

type Resource =
    {
        Effects : Dictionary<string, Effect>;
        Sounds : Dictionary<string, SoundEffect>;
        Models : Dictionary<string, Model>;
//        BoneTransforms : Matrix[];
        Fonts : Dictionary<string, SpriteFont>;
        SpriteBatch : Dictionary<string, SpriteBatch>;
        Texture2D : Dictionary<string, Texture2D>;
    }
    with
    
    static member Default =
        {
            Effects = new Dictionary<string, Effect>();
            Sounds = new Dictionary<string, SoundEffect>();
            Models = new Dictionary<string, Model>();
//            BoneTransforms = Array.init 1 (fun i -> Matrix());
            Fonts = new Dictionary<string, SpriteFont>();
            SpriteBatch = new Dictionary<string, SpriteBatch>();
            Texture2D = new Dictionary<string, Texture2D>();
        }
        
    member this.Load(xna : XNAEngine) =
        this.SpriteBatch.["hud"] <- new SpriteBatch(xna.gd)
        this.Texture2D.["circle"] <- Texture2D.FromFile(xna.gd, "circle.png")
    