using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class RenbanConstraint : ISudokuConstraint
    {
        private readonly HashSet<RowCol> renbanCells;

        public RenbanConstraint(params RowCol[] renbanCells)
        {
            this.renbanCells = renbanCells.ToHashSet();
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol)
        {
            if (this.AffectsCell(rowCol)) {
                return this.renbanCells.Where(i => i != rowCol);
            }

            return Enumerable.Empty<RowCol>();
        }

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                var vals = this.renbanCells.Select(i => i == rowCol ? value : grid.Get(i)).Where(i => i != ISudokuGrid.EmptyValue);

                return !vals.HasDuplicates() && vals.Max() - vals.Min() < this.renbanCells.Count;
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid)
        {
            if (this.renbanCells.Any(grid.IsEmpty)) {
                return false;
            }

            return this.renbanCells.Select(grid.Get).Order().IsConsecutive();
        }

        private bool AffectsCell(RowCol rowCol) =>
            this.renbanCells.Contains(rowCol);
    }
}
