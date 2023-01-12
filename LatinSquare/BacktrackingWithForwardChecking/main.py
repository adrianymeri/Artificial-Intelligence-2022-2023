import LatinSquare as ls
import numpy as np

def backtracking(index, square):  # indeksi duhet te filloje me 0
    sq.show_square_as_matrix()
    if index == square.length:  # mbarimi i tabeles
        return True
    # provimi i te gjitha vlerave te mundshme
    for i in range(1, square.size + 1, 1):
        a = square.squares
        b = square.temp_change_value(i, index).squares
        if square.is_latin_valid(square.temp_change_value(i, index), index):  # nese katrori do te jete valid pas ndryshimit
            square.squares[index] = i  # nese eshte valid pas ndryshimit - bej ndryshimin 
            if backtracking(index + 1, square):  # vazhdimi i kontrollimit te ketij path-i derisa mberrin ne fund
                return True
    square.squares[index] = 0

def backtracking_with_forward_checking(index, square):  # indeksi duhet te filloje me 0 
    sq.show_square_as_matrix()
    print(" CHECKED: ", square.checked)
    if index == square.length:  # mbarimi i tabeles
        return True
    # provimi i te gjitha vlerave te mundshme
    for i in range(1, square.size + 1, 1):
        if square.checked[i - 1] == 1:
            continue
        if square.is_latin_valid(square.temp_change_value(i, index), index):  # nese katrori do te jete valid pas ndryshimit
            if square.squares[index] != 0:  # nese eshte valid por vendi eshte zene me heret 
                square.checked[square.squares[index] - 1] = 0
            square.squares[index] = i  # nese eshte valid pas ndryshimit - bej ndryshimin 
            if (index + 1) % square.size == 0:
                square.checked = np.zeros(square.size, int)
            else:
                square.checked[i - 1] = 1
            if backtracking_with_forward_checking(index + 1, square):  # vazhdimi i kontrollimit te ketij path-i derisa mberrin ne fund
                return True
    square.squares[index] = 0  # undo


n = int(input("Jepni n= "))

sq = ls.LatinSquare(n)
backtracking(0, sq)
backtracking_with_forward_checking(0, sq)
sq.show_square_as_matrix()
