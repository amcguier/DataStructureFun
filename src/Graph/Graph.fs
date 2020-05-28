namespace Datastructure

module Graph =

  [<Struct>]
  type GraphEdge =
    {
      Node : string
      Weight : int option
    }

  [<Struct>]
  type GraphNode =
    {
      Label : string
      References : GraphEdge list
    }

  
  type Graph =
    {
      IsDirected : bool
      IsWeighted : bool
      Nodes : Map<string,GraphNode>
    }

   type GraphResult<'a> = Graph * 'a
