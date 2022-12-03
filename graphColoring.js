/**
 * Graph Coloring
 * Atlantik Limani, Dardan Bakiu, Bajram Sherifi
 */

const graph = new Map()

const colors = [0,1,2];

graph.set(1, {'neighbor': [4,2], 'color': null});
graph.set(2, {'neighbor': [1,3], 'color': null});
graph.set(3, {'neighbor': [4,2], 'color': null});
graph.set(4, {'neighbor': [1,3], 'color': null});


//kjo kontrollon a e kan ate ngjyre neighboors
function checkNeighbor(node) {
  node = graph.get(node)
  for(let i = 0; i < node.neighbor.length; i++) {
    if(graph.get(node.neighbor[i]).color == node.color) {
      return false
    }
  }

  return true;
}

function colorNode(node) {
  graph.get(node).color = colors[Math.floor(Math.random() * 3)]
  while (!checkNeighbor(node)) {
    graph.get(node).color = colors[Math.floor(Math.random() * 3)]
  } 
}

function colorGraph(numberOfNodes) {
  const randomStartNodeIndex = Math.floor((Math.random() * numberOfNodes) + 1)

  for(let i = 1; i <= numberOfNodes; i++) {
    colorNode(i)
  } 

  console.log(graph)
}
colorGraph(4)
