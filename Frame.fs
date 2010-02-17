namespace Brains

open Microsoft.Xna.Framework

type Frame =
    abstract member Draw : float32  -> unit
    abstract member Update : float32 -> unit

