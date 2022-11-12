using System;
using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class RegionConstraint : ISudokuConstraint
    {
        private readonly IEnumerable<RowCol> cells;

        public RegionConstraint(IEnumerable<RowCol> cells)
        {
            this.cells = cells ?? throw new ArgumentNullException(nameof(cells));
        }

        public IEnumerable<RowCol> AffectedCells() =>
            this.cells;

        public bool AffectsCell(RowCol rowCol) =>
            this.cells.Any(i => rowCol == i);

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                if (grid.IsEmpty(rowCol)) {
                    return this.cells.All(i => grid.Get(rowCol) != value);
                }

                return false;
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid) =>
            !this.cells.Select(grid.Get).HasDuplicates();
    }
}
