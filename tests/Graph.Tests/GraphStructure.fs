module GraphStructure

open Expecto
open Expecto.ExpectoFsCheck
open FsCheck

open DataStructures.Graph

let config = { FsCheck.Config.Default with MaxTest = 10000 }


let unique =
  Arb.Default.NonEmptyString().Generator
  |> Gen.map (function
       | NonEmptyString(x) -> x)
  |> Gen.listOf
  |> Gen.map (fun x ->
       x
       |> Set.ofList
       |> List.ofSeq)
  |> Arb.fromGen

type UniqueGenerators =
  static member nonNullString() = unique


let uniqueConfig = { FsCheckConfig.defaultConfig with arbitrary = [ typeof<UniqueGenerators> ] }

let firstArgIsDirected =
  testProperty "First Argument is IsDirected" (fun directed weighted ->
    let g = initialize directed weighted
    g.IsDirected = directed)

let secondArgIsWeighted =
  testProperty "Second Argument is weighted" (fun directed weighted ->
    let g = initialize directed weighted
    g.IsWeighted = weighted)

let insertAndExtract graph str =
  addNode graph str
  |> function
  | Ok(g) -> g
  | x ->
      Expect.isOk x "Unable to add node"
      graph

let nodesAddedSuccessfully =
  testPropertyWithConfig uniqueConfig "Multiple nodes are added" (fun labels ->
    let finalGraph =
      initialize false false
      |> List.fold insertAndExtract
      <| labels
    List.iter (fun lbl ->
      finalGraph.Nodes.ContainsKey lbl
      |> Expect.isTrue
      <| sprintf "%s not found in graph after insert" lbl) labels
    true)



[<Tests>]
let tests = testList "Graph Structure" [ firstArgIsDirected; secondArgIsWeighted; nodesAddedSuccessfully ]
