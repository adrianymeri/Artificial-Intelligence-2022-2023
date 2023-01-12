import numpy as np
import math

class LatinSquare:

    def __init__(self, size):
        self.size = size
        self.length = size**2
        self.squares = np.zeros(size ** 2, int)
        self.checked = np.zeros(size, int)

    def get_size(self):
        return self.size

    def get_square(self):
        return self.squares

    def show_square(self):
        print(self.squares)

    def show_square_as_matrix(self):
        print("\n\n\nLATIN SQUARE:")
        for i in range(0, self.length, self.size):
            print("\n")
            for j in range(i, i+self.size, 1):
                print(self.squares[j], end=' ')

    def get_field(self, index):
        row = math.ceil((index+1)/self.size)-1
        col = index % self.size
        return row, col

    def get_row(self, index):
        return math.ceil((index+1)/self.size)-1

    def get_column(self, index):
        return index % self.size

    def get_index(self, row, column):
        return row * self.size + column

    @staticmethod
    def is_latin_valid(square, index):

        for i in range(square.length - 1):  # i = next index
            row_to_compare = square.get_row(i)
            column_to_compare = square.get_column(i)

            if square.get_row(index) == row_to_compare or square.get_column(index) == column_to_compare:
                if square.squares[index] == square.squares[square.get_index(row_to_compare, column_to_compare)] and index is not i:
                    return False
        return True

    def temp_change_value(self, val, index):
        temp = LatinSquare(self.size)
        temp.squares = [self.squares[i] for i in range(self.length)]
        temp.squares[index] = val
        return temp
