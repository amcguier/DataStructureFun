namespace DataStructures

module Graph =

  [<Struct>]
  type GraphEdge =
    { Node: string
      Weight: int option }

  [<Struct>]
  type GraphNode =
    { Label: string
      References: GraphEdge list }

  [<Struct>]
  type Graph =
    { IsDirected: bool
      IsWeighted: bool
      Nodes: Map<string, GraphNode> }

  type GraphResult<'a> = Graph * 'a

  let initialize isDirected isWeighted =
    { IsDirected = isDirected
      IsWeighted = isWeighted
      Nodes = Map.empty }

  let addNode graph label =
    if Map.containsKey label graph.Nodes then
      Error("Can't add a node where one already exists")
    else
      let node =
        { Label = label
          References = [] }
      { graph with Nodes = Map.add label node graph.Nodes } |> Ok
