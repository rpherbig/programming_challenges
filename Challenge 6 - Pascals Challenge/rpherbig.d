// Copyright: Robert P Herbig, 2011

// Interpreter at http://ideone.com/

int pascalsTriangle(in int row, in int column) {
    int value = 1;
    int target  = (column >= row + 1) ? row + 1 - column : column;
    foreach (c; 0 .. target) {
        // http://en.wikipedia.org/wiki/Pascal's_triangle#Calculating_an_individual_row_or_diagonal_by_itself
        value = (value * (row - c)) / (c + 1);
    }
    return value;
}
 
void main() {
    assert(pascalsTriangle(0, 0) == 1);
    assert(pascalsTriangle(1, 1) == 1);
    assert(pascalsTriangle(2, 1) == 2);
    assert(pascalsTriangle(4, 2) == 6);
    assert(pascalsTriangle(9, 4) == 126);
}
