
type Microsoft.FSharp.Collections.FSharpList<'a> with
    static member iterWhile (f:'a -> bool) (ls:'a list) = 
        let rec iterLoop f ls = 
            match ls with
            | head :: tail -> if f head then iterLoop f tail
            | _ -> ()
        iterLoop f ls

open AI

type AI_Chain(ais : AI list) =
    inherit AI()
    
    let mutable started = []
        
    override this.Start() =
        base.Start()
        match ais with
        | head :: tail -> 
            head.Start()
            started <- head :: started
        | _ -> ()
    override this.Stop() =
        base.Stop()
        started |> List.iter (fun x -> x.Stop())
        started <- []
    override this.Reset() =
        base.Reset()
    override this.Priority() =
        match started with
        | head :: tail -> head.Priority()
        | _ -> 0.0
    override this.Update(gameTime) =
        ais
        |> List.iterWhile (fun x -> 
            if not x.Running then 
                x.Start()
                started <- x :: started
            x.Update(gameTime)
            x.Priority() > 0.0)
            