using System.Collections;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class RowsConstraint : BaseSudukuConstraint, ISudokuConstraint
    {
        public override bool Validate(ISudokuGrid grid)
        {
            for (int row = 1; row <= grid.Size; row++) {

                BitArray used = new BitArray(grid.Size + 1);
                for (int col = 1; col <= grid.Size; col++) {

                    if (!grid.IsEmpty(row, col)) {
                        if (used[grid.Get(row, col)]) {
                            return false;
                        }
                        else {
                            used[grid.Get(row, col)] = true;
                        }
                    }
                }
            }
            return true;
        }

        public override bool IsValid(ISudokuGrid grid, int row, int col, int value)
        {
            for (int c = 1; c <= grid.Size; c++) {
                if (c != col && grid.Get(row, c) == value) {
                    return false;
                }
            }

            return true;
        }
    }
}
