using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class ColumnsConstraint : ISudokuConstraint
    {
        private readonly List<RegionConstraint> columns;

        public ColumnsConstraint(int size)
        {
            this.columns = Enumerable.Range(1, size)
                                     .Select(col => new RegionConstraint(Enumerable.Range(1, size).Select(row => new RowCol(row, col))))
                                     .ToList();
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol) =>
            this.columns[rowCol.Col - 1].AffectedCells(rowCol);

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            this.columns[rowCol.Col - 1].IsValidPlacement(grid, rowCol, value);

        public bool Validate(ISudokuGrid grid) =>
            this.columns.All(r => r.Validate(grid));
    }
}
