﻿open System
open Microsoft.Xna.Framework

open Brains
open Mouse

type MyGame() as this =
    inherit Game()
    
    let xna = XNAEngine()
    let resource = Resource.Default
    
    let mutable frames = List<Frame>.Empty
    let mutable oldTime = 0L
    let mutable diffTime = 0.0f

    do  xna.Init(this)

    override this.LoadContent() =
        resource.Load(xna)
        
    override this.Initialize() =
        base.Initialize()
        frames <- (Stage_1(resource) :> Frame) :: frames       
        
    override this.Draw(gameTime) =
        xna.clear()
        frames |> List.iter (fun f -> f.Draw(diffTime))
        

    override this.Update(gameTime) =
        let currentTime = gameTime.TotalRealTime.Ticks
        
        if oldTime = 0L then
            oldTime <- currentTime
            
        if currentTime - oldTime > 10000000L / 80L then
            diffTime <- 10000000.0f / float32 (currentTime - oldTime)
            Mouse.Update()
            frames |> List.iter (fun f -> f.Update(1.0f / 80.0f))//diffTime))
            oldTime <- currentTime
    
let main() =
    let game = new MyGame()
    game.Run()

[<STAThread>]
do main()
