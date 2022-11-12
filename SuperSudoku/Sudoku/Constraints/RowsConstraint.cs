using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class RowsConstraint : ISudokuConstraint
    {
        private readonly List<RegionConstraint> rows;

        public RowsConstraint(int size)
        {
            this.rows = Enumerable.Range(1, size)
                                  .Select(row => new RegionConstraint(Enumerable.Range(1, size).Select(col => new RowCol(row, col))))
                                  .ToList();
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol) =>
            this.rows[rowCol.Row - 1].AffectedCells(rowCol);

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            this.rows[rowCol.Row - 1].IsValidPlacement(grid, rowCol, value);

        public bool Validate(ISudokuGrid grid) =>
            this.rows.All(r => r.Validate(grid));
    }
}
