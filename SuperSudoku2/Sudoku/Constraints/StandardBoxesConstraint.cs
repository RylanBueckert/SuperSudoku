using System.Collections;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class StandardBoxesConstraint : BaseSudukuConstraint, ISudokuConstraint
    {
        public override bool Validate(ISudokuGrid grid)
        {
            for (int bi = 0; bi < grid.Size; bi += grid.Size / 3) {
                for (int bj = 0; bj < grid.Size; bj += grid.Size / 3) {

                    BitArray used = new BitArray(grid.Size + 1);
                    for (int i = 0; i < grid.Size / 3; i++) {
                        for (int j = 0; j < grid.Size / 3; j++) {
                            if (!grid.IsEmpty(bi + i, bj + j) && used[grid.Get(bi + i, bj + j)]) {
                                return false;
                            }
                            used[grid.Get(bi + i, bj + j)] = true;
                        }
                    }
                }
            }
            return true;
        }

        public override bool IsValid(ISudokuGrid grid, int row, int col, int value)
        {
            int boxr = (row - 1) / 3 * 3 + 1;  // coordinates of the top left cell of the box
            int boxc = (col - 1) / 3 * 3 + 1;
            for (int i = boxr; i < boxr + grid.Size / 3; i++) {
                for (int j = boxc; j < boxc + grid.Size / 3; j++) {
                    if ((i != row || j != col) && grid.Get(i, j) == value) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
