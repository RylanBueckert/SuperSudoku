using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public abstract class BaseSudukuConstraint : ISudokuConstraint
    {
        public abstract bool Validate(ISudokuGrid grid);

        public virtual bool IsValid(ISudokuGrid grid, int row, int col, int value)
        {
            if (this.AffectsCell(grid, row, col)) {
                if (grid.Set(row, col, value)) {
                    bool result = this.Validate(grid);
                    grid.Clear(row, col);
                    return result;
                }

                return false;
            }

            return true;
        }

        public virtual bool AffectsCell(ISudokuGrid grid, int row, int col) =>
            true;
    }
}
