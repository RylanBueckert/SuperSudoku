using System.Collections.Generic;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public interface ISudokuConstraint
    {
        bool Validate(ISudokuGrid grid);

        bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value);

        IEnumerable<RowCol> AffectedCells(RowCol rowCol);
    }
}
