using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public interface ISudokuConstraint
    {
        public bool Validate(ISudokuGrid grid);

        public bool IsValid(ISudokuGrid grid, int row, int col, int value);
    }
}
