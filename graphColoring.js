/**
 * Graph Coloring
 * Atlantik Limani, Dardan Bakiu, Bajram Sherifi
 */

function findMaxEdges(graph) {
  let max = 0;
  graph.forEach(node => {
    console.log(node)
    if(max < node.neighbor.length) {
      max = node.neighbor.length
    }
  });

  return max + 1;
}

function generateColors(graph) {
  let colors = []
  for (let i = 0; i < findMaxEdges(graph); i++) {
    colors.push(i);
  }
  return colors;
}

const graph = new Map()

graph.set(1, {'neighbor': [4,2], 'color': null});
graph.set(2, {'neighbor': [1,4], 'color': null});
graph.set(3, {'neighbor': [4,5], 'color': null});
graph.set(4, {'neighbor': [1,2,5,3], 'color': null});
graph.set(5, {'neighbor': [4,3], 'color': null});

const colors = generateColors(graph);

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
  graph.get(node).color = colors[Math.floor(Math.random() * colors.length)]
  while (!checkNeighbor(node)) {
    graph.get(node).color = colors[Math.floor(Math.random() * colors.length)]
  } 
}

function colorGraph(numberOfNodes) {
  const randomStartNodeIndex = Math.floor((Math.random() * numberOfNodes) + 1)

  for(let i = 1; i <= numberOfNodes; i++) {
    colorNode(i)
  } 

  console.log(graph)
}

console.log('numri i ngjyrave : ', colors)
colorGraph(5)
