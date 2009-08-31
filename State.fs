
open Microsoft.FSharp.Math

type State() =
    let state = Matrix.Generic.init 4 4 (fun x y -> if x > y then 0 else 0)