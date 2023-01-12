from itertools import combinations,chain

# backtracking

def arrangeTables(golfers, tables, alreadyPaired):
    
    result        = [[]] * tables 
    tableNumber   = 0
    allGolfers    = set(range(1,golfers+1))
    foursomes     = [combinations(allGolfers,4)]
    
    while True:
        
        foursome = next(foursomes[tableNumber],None)

        if foursome:  
        
            foursome = sorted(foursome)
            pairs    = set(combinations(foursome,2))

            # nese nuk jane taku ata golfers e merr ate kombinim
            if pairs.isdisjoint(alreadyPaired):
            
                result[tableNumber] = foursome
                tableNumber += 1
                
                if tableNumber == tables:
                    break
                
                remainingGolfers = allGolfers - set(chain(*result[:tableNumber]))
                foursomes.append(combinations(remainingGolfers,4))
                
            continue

        else:
            
            tableNumber -= 1
            foursomes.pop()
            
            if tableNumber < 0:
                return None
            
            continue
        
    return result

def tournamentTables(golfers, tables=None):
    
    tables  = tables or golfers//4
    rounds  = []    # lista e katersheve per secilin round
    paired  = set() # tuples te golfers
    
    while True:

        # derisa te ekzistojne kombinime te mundshme,
        # thirret funksioni arrangeTables
        roundTables = arrangeTables(golfers,tables,paired)
        
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
