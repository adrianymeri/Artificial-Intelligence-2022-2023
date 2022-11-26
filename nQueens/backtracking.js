const N = 8
 
function printSolution(board)
{
    for(let i = 0; i < N; i++)
    {   
        let row = board[i].join(' ');
        console.log(row);
    }
}
 
function hasConflicts(board, row, col)
{
    for(let i = 0; i < col; i++){
        if(board[row][i] == 1)
            return false  
    }
  
    for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
        if (board[i][j])
            return false
  
    for (i = row, j = col; j >= 0 && i < N; i++, j--)
        if (board[i][j])
            return false
  
    return true
}
 
 
function solveNQUtil(board, col) {
    if(col >= N)
        return true
  
    for(let i=0;i<N;i++){
  
        if(hasConflicts(board, i, col)==true){
              
            board[i][col] = 1
            if(solveNQUtil(board, col + 1) == true)
                return true
  
            board[i][col] = 0
        }
    }

    return false
}
 
function matrix(m, n) {
    return Array.from({
      length: m
    }, () => new Array(n).fill(0));
};
 
function solveNQ(){
    let board = matrix(N,N);
  
    if(solveNQUtil(board, 0) == false){
        console.log("Solution does not exist")
        return false
    }
  
    printSolution(board)
    return true
}
 
solveNQ()