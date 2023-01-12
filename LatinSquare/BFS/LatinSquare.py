from queue import Queue
import copy
import time

class Problem(object):

    def __init__(self, initial):
        self.initial = initial
        self.size = len(initial)
        self.height = int(self.size/3) 

    # Kthimi i nje seti te numrave valid nga vlerat qe nuk shfaqen ne vlera te
    # perdorura
    
    def filter_values(self, values, used):
        return [number for number in values if number not in used]

    # Kthimi i nje hapesire te zbrazet
    def get_spot(self, board, state):
        for row in range(board):
            for column in range(board):
                if state[row][column] == 0:
                    return row, column   

    def actions(self, state):
        number_set = range(1, self.size+1) # Definimi i setit te numrave valid qe mund te vendosen ne tabele
        in_column = [] # Lista e vlerave valide ne hapesiren e kolones

        row,column = self.get_spot(self.size, state) # Marrja e hapesires se pare te zbrazet ne tabele

        # Filtrimi i vlerave valide duke u bazuar ne rresht
        in_row = [number for number in state[row] if (number != 0)]
        options = self.filter_values(number_set, in_row)

        # Filtrimi i vlerave valide duke u bazuar ne kolone
        for column_index in range(self.size):
            if state[column_index][column] != 0:
                in_column.append(state[column_index][column])
        options = self.filter_values(options, in_column)

        for number in options:
            yield number, row, column      

    # Kthen tabelen e perditesuar pas shtimit te vleres valide 
    def result(self, state, action):

        play = action[0]
        row = action[1]
        column = action[2]

        # Shtimi i nje vlere valide ne tabele
        new_state = copy.deepcopy(state)
        new_state[row][column] = play

        return new_state

    def check_legal(self, state):

        total = sum(range(1, self.size+1))

        for row in range(self.size):
            if (len(state[row]) != self.size) or (sum(state[row]) != total):
                return False

            column_total = 0
            for column in range(self.size):
                column_total += state[column][row]

            if (column_total != total):
                return False
            
        return True

class Node:

    def __init__(self, state, action=None):
        self.state = state
        self.action = action

    # Krijimi i nje gjendje te re te tabeles
    def expand(self, problem):
        return [self.child_node(problem, action)
                for action in problem.actions(self.state)]

    # Kthimi i nyjes me gjendjen e re te tabeles
    def child_node(self, problem, action):
        next = problem.result(self.state, action)
        return Node(next, action)

def BFS(problem):

    node = Node(problem.initial)

    if problem.check_legal(node.state):
        return node

    frontier = Queue()
    frontier.put(node)

    # Unaza derisa te gjitha nyjet eksplorohen ose derisa te gjendet zgjidhja
    while (frontier.qsize() != 0):

        node = frontier.get()
        for child in node.expand(problem):
            if problem.check_legal(child.state):
                return child

            frontier.put(child)

    return None

def BFS_solve(board):

    start_time = time.time()

    problem = Problem(board)
    solution = BFS(problem)
    elapsed_time = time.time() - start_time

    if solution:
        print ("Gjetja e zgjidhjes")
        for row in solution.state:
            print (row)
    else:
        print ("Nuk ka zgjidhje te mundshme")

    print ("Koha e matur: " + str(elapsed_time) + " sekonda")

grid = [[0,0,0,0,0],
      [0,0,0,0,0],
      [0,0,0,0,0],
      [0,0,0,0,0],
      [0,0,0,0,0]]

BFS_solve(grid)
