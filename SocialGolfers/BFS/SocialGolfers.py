from random import choice

class Person:
    def __init__(self, name):
        self.name = name
        self.met = set()

    def __repr__(self):
        return self.name

class TooManyIteration(Exception):
    pass

GAMES = 6
# funksionon edhe per 7 raunde
TEAMS = 8
TEAMSIZE = 4

def pick_teams(s):
    s_ = s.copy()
    teams = [set() for _ in range(TEAMS)]
    for t in teams:
        c = 0
        while len(t) < TEAMSIZE:
            p = choice(tuple(s_))
            s_.remove(p)

            if all(p not in x.met for x in t):
                t.add(p)
                
            else:
                s_.add(p)

                c += 1
                if c > len(s):
                    # deshtimi ne zgjedhje
                   raise TooManyIteration()

    for t in teams:
        for x in t:
            x.met.update(t)

    return teams

s = set(Person(str(i)) for i in range(32))

games = []

while len(games) < GAMES:
    try:
        games.append(pick_teams(s))
    except TooManyIteration:
        continue
    
print(games)
