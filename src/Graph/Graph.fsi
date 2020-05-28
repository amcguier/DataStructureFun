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


  val initialize: bool -> bool -> Graph

  val addNode: Graph -> string -> Result<Graph, string>
