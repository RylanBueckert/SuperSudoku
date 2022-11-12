using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class RegionConstraint : ISudokuConstraint
    {
        protected readonly HashSet<RowCol> cells;

        public RegionConstraint(IEnumerable<RowCol> cells)
        {
            cells.ThrowArgIfNull(nameof(cells));

            this.cells = cells.ToHashSet();
        }

        public virtual IEnumerable<RowCol> AffectedCells(RowCol rowCol)
        {
            if (this.AffectsCell(rowCol)) {
                return this.cells;
            }

            return Enumerable.Empty<RowCol>();
        }

        public virtual bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                return this.cells.All(i => grid.Get(i) != value || i == rowCol);
            }

            return true;
        }

        public virtual bool Validate(ISudokuGrid grid) =>
            !this.cells.Select(grid.Get).HasDuplicates();

        protected bool AffectsCell(RowCol rowCol) =>
            this.cells.Contains(rowCol);
    }
}
