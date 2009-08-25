
type Tree<'a>() =
    let siblings = new ResizeArray<'a>()
    let children = new ResizeArray<'a>()

    member this.Siblings
        with get() = siblings
        
    member this.hasSiblings() =
        siblings.Count <> 0
        
    member this.hasChildren() =
        children.Count <> 0
        
    member this.Children
        with get() = children

    member this.addSibling(sibling : 'a) =
        siblings.Add(sibling)
        
    member this.addChild(child : 'a) =
        children.Add(child)