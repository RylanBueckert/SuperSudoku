using System.Collections.Generic;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public interface ISudokuConstraint
    {
        bool Validate(ISudokuGrid grid);

        bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value);

        bool AffectsCell(RowCol rowCol);

        IEnumerable<RowCol> AffectedCells();
    }
}
