using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public interface ISudokuConstraint
    {
        bool Validate(ISudokuGrid grid);

        bool IsValid(ISudokuGrid grid, int row, int col, int value);
    }
}
