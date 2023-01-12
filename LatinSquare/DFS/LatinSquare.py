import copy
import time

class Variable:
    def __init__(self, cur_value, domain):
       
        self.cur_value = cur_value
        self.domain = domain


class ProblemState:
    def __init__(self, variables, consistent):
        
        self.variables = variables
        self.consistent = consistent

    def is_consistent(self):
      
        return self.consistent

    def to_string(self, sq_size):
        for i in range(sq_size):
            for j in range(sq_size):
                print(str(self.variables[(i, j)].cur_value) + " ", end='')
            print("")


class Problem:
    def __init__(self, latin_sq, square_size):
        self.latin_sq = latin_sq
        self.sq_size = square_size

    def is_goal(self, state):
        """
        Kontrollimi nese gjendja eshte Latin Sqaure Goal
        """

        # nuk mund te kete variabel te zbrazet
        for a_key in state.variables:
            if state.variables.get(a_key).cur_value == 0:
                return False

        # nese state eshte inkonsistente nuk eshte zgjidhje
        if not state.is_consistent():
            print("here!")
            return False

        possible_values = set(range(1, self.sq_size + 1))  # {1, 2, 3, ... N}
        # iterimi neper te gjitha kolonat dhe rreshtat
        for i in range(self.sq_size):
            col = set(state.variables[(i, j)].cur_value for j in range(self.sq_size))
            if possible_values != col:
                return False
            row = set(state.variables[(j, i)].cur_value for j in range(self.sq_size))
            if possible_values != row:
                return False

        return True

    def get_actions(self, state):
        """
        Marrja e nje seti te veprimeve te mundshme per zbrazetiren e ardhshme
        """

        if state.is_consistent() is False:
            return {}  
        else:
            for i in range(self.sq_size):
                for j in range(self.sq_size):
                    if state.variables[(i, j)].cur_value == 0:
                        actions = state.variables[(i, j)].domain  # {1, 2, 3, ... N}
                        return actions

        return {}  

    def results(self, state, act):

        new_state = state

        for i in range(self.sq_size):
            for j in range(self.sq_size):
                if new_state.variables[(i, j)].cur_value == 0:
                    new_state.variables[(i, j)].cur_value = act
                    for k in range(self.sq_size):

                        if act in new_state.variables[(i, k)].domain:
                            new_state.variables[(i, k)].domain.remove(act)
                            if len(new_state.variables[(i, k)].domain) == 0 \
                                    and new_state.variables[(i, k)].cur_value == 0:
                                new_state.consistency = False

                        if act in new_state.variables[(k, j)].domain:
                            new_state.variables[(k, j)].domain.remove(act)
                            if len(new_state.variables[(k, j)].domain) == 0 \
                                    and new_state.variables[(k, j)].cur_value == 0:
                                new_state.consistency = False
                    break
            else:  
                continue
            break

        return new_state

    def restrict_domain(self, a_state, acts, coords):

        col = set(self.latin_sq[coords[0]])  
        row = set(self.latin_sq[x][coords[1]] for x in range(self.sq_size))  

        restricted_domain = acts - col
        restricted_domain = restricted_domain - row
        if len(restricted_domain) == 0:
            a_state.consistent = False

        return set(restricted_domain)

    def initial_state(self):
        
        state = ProblemState(dict(), True)

        actions = set(range(1, self.sq_size + 1))
        for i in range(self.sq_size):
            for j in range(self.sq_size):
                if self.latin_sq[i][j] != 0:
                    state.variables[(i, j)] = Variable(self.latin_sq[i][j], {None})
                else:
                    state.variables[(i, j)] = Variable(self.latin_sq[i][j], set(self.restrict_domain(state, actions, (i, j))))

        return state


class TreeSearch:
    def __init__(self):
        pass

    def depth_first(self, square, size):
     
        nodes = 0
        s_time = time.time()
        front_size = 0

        prob = Problem(square, size)
        initial_state = prob.initial_state()
        frontier = [initial_state]

        if prob.is_goal(initial_state):  
            t = time.time() - start_time
            print("Time:", str(t) + "s")
            print("Nodes Checked:", nodes)
            return True

        while len(frontier) > 0:
            last = frontier.pop()  # LIFO - ben pop node-in e fundit ne frontier

            if prob.is_goal(last):  # ben check per zgjidhjen
                last.to_string(prob.sq_size)
                t = time.time() - s_time
                print("Time:", str(t) + "s")
                print("Nodes Checked:", nodes)
                return True

            nodes += 1  # rrit numrin e nodes te kontrolluara

            for x in range(prob.sq_size):
                for y in range(prob.sq_size):
                    if last.variables[(x, y)].cur_value == 0:
                        actions = prob.get_actions(last)
                        for an_act in actions:
                            new_state = prob.results(copy.deepcopy(last), an_act)
                            frontier.append(new_state)
                            front_size = max(len(frontier), front_size)

            if (time.time() - s_time) >= 10.0:
                break

        print("Failure.")
        t = time.time() - s_time
        print("Time:", str(t) + "s")
        print("Nodes Checked:", nodes)
        return False


fin = 'LatinSquares.txt'
print("Data Set:", fin)

# Gjeneron nje liste te Latin Squares
file_data = []
L = []
size = []
counting_size = 0
with open(fin) as f:
    number_of_problems = f.readline().split()
    number_of_problems = int(number_of_problems[0])
    for p in range(number_of_problems):
        file_data.clear()  
        size.append(int(f.readline().split()[0]))
        for i in range(size[p]):
            square = f.readline().split()
            file_data.append(square) 
        L.append(copy.deepcopy(file_data))  
        f.readline()

f.close()
# kthen '_' ne 0 ('_' per vende te zbrazeta)
count = []
for x in range(len(L)):
    count.append(0)
    for y in range(len(L[x])):
        for z in range(len(L[x][y])):
            if L[x][y][z] == '_':
                L[x][y][z] = 0
                count[x] += 1
            L[x][y][z] = int(L[x][y][z])

timeo = 0
for x in range(number_of_problems):
    start_time = time.time()
    search = TreeSearch()
    solve = search.depth_first(L[x], size[x])
    if solve:
        print("")
    else:
        print("")
    timeo += (time.time() - start_time)
