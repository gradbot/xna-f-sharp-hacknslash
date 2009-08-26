open System
open System.Collections

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Audio

open XNAEngine
open Resource
open Entity
open Frame
open Stage_1

type MyGame() as this =
    inherit Game()
    
    let xna = XNAEngine()
    let resource = Resource.Default
    
    let mutable frames = List<Frame>.Empty
    let mutable oldTime = 0.0f
    let mutable diffTime = 0.0f

    do  xna.Init(this)

    override this.LoadContent() =
        resource.Load(xna)
        
    override this.Initialize() =
        base.Initialize()
        frames <- (Stage_1(resource) :> Frame) :: frames       
        
    override this.Draw(gameTime) =
        xna.gd.Clear(Color(0.0f, 0.0f, 0.0f))
        frames |> List.iter (fun f -> f.Draw(diffTime))
        

    override this.Update(gameTime) =
        let currentTime = float32 gameTime.TotalRealTime.TotalSeconds
        if oldTime = 0.0f then
            oldTime <- currentTime
        diffTime <- currentTime - oldTime            
            
        frames |> List.iter (fun f -> f.Update(diffTime))
        //this.Exit()            
        oldTime <- currentTime
    
let main() =
    let game = new MyGame()
    game.Run()

[<STAThread>]
do main()