using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class RegionConstraint : ISudokuConstraint
    {
        private readonly HashSet<RowCol> cells;

        public RegionConstraint(IEnumerable<RowCol> cells)
        {
            cells.ThrowArgIfNull(nameof(cells));

            this.cells = cells.ToHashSet();
        }

        public IEnumerable<RowCol> AffectedCells() =>
            this.cells;

        public bool AffectsCell(RowCol rowCol) =>
            this.cells.Contains(rowCol);

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                if (grid.IsEmpty(rowCol)) {
                    return this.cells.All(i => grid.Get(i) != value);
                }

                return false;
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid) =>
            !this.cells.Select(grid.Get).HasDuplicates();
    }
}
