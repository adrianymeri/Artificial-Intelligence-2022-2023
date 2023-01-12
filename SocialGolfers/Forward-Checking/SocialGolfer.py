from itertools import combinations,chain

# forward checking

def arrangeTables(golfers, tables, alreadyPaired):
    
    result        = [[]] * tables 
    tableNumber   = 0
    allGolfers    = set(range(1,golfers+1))
    foursomes     = [combinations(allGolfers,4)]
    
    while True:
        
        foursome = next(foursomes[tableNumber],None)
        
        if not foursome:
            tableNumber -= 1
            foursomes.pop()
            
            if tableNumber < 0:
                return None
            
            continue
        
        foursome = sorted(foursome)
        pairs    = set(combinations(foursome,2))

        # nese veq jane taku ata golfers njehere nuk e merr ate kombinim
        if not pairs.isdisjoint(alreadyPaired):
            continue
        
        result[tableNumber] = foursome
        tableNumber += 1
        
        if tableNumber == tables:
            break
        
        remainingGolfers = allGolfers - set(chain(*result[:tableNumber]))
        foursomes.append(combinations(remainingGolfers,4))
        
    return result

def tournamentTables(golfers, tables=None):
    
    tables  = tables or golfers//4
    rounds  = []    # lista e katersheve per secilin round
    paired  = set() # tuples te golfers
        
    while True:

        if tables == 0 or golfers % 4 != 0 or golfers < 0:
            print('Nuk ka kombinime te mundshme per kete hyrje!')
            break

        # derisa te ekzistojne kombinime te mundshme,
        # thirret funksioni arrangeTables
        roundTables = arrangeTables(golfers,tables,paired)

        # ketu sikur te backtracking behet check derisa ka roundtables
        # kur nuk ka roundtables, perkatesisht kombinime ndalon programin
        if not roundTables:
            break
      
        rounds.append(roundTables)
        
        for foursome in roundTables:
            pairs = combinations(foursome,2)
            paired.update(pairs)
            
    return rounds

# shfaqja e katersheve te gjeneruara
# ne kete rast rezultati eshte fituar per 5 jave
for roundNumber,roundTables in enumerate(tournamentTables(32)):
    print("Java ", roundNumber+1, ":", roundTables)
