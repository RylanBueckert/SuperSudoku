using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class ArrowConstraint : ISudokuConstraint
    {
        private readonly RowCol sumCell;

        private readonly HashSet<RowCol> arrowCells;

        public ArrowConstraint(RowCol sumCell, IEnumerable<RowCol> arrowCells)
        {
            this.sumCell = sumCell.ThrowArgIfNull(nameof(sumCell));
            this.arrowCells = arrowCells.ThrowArgIfNull(nameof(arrowCells)).Where(i => i != null).ToHashSet();
        }

        public IEnumerable<RowCol> AffectedCells() =>
            this.arrowCells.Append(this.sumCell);

        public bool AffectsCell(RowCol rowCol) =>
            rowCol == this.sumCell || this.arrowCells.Contains(rowCol);

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                if (grid.IsEmpty(rowCol)) {
                    if (rowCol == this.sumCell) {
                        return this.GetMaxArrowSum(grid) >= value && this.GetMinArrowSum(grid) <= value;
                    }
                    else {
                        grid.Set(rowCol, value);
                        int min = this.GetMinArrowSum(grid);
                        int max = this.GetMaxArrowSum(grid);
                        grid.Clear(rowCol);

                        if (grid.IsEmpty(this.sumCell)) {
                            return min <= grid.Size;
                        }
                        else {
                            int target = grid.Get(this.sumCell);
                            return min <= target && max >= target;
                        }
                    }
                }

                return false;
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid)
        {
            if (grid.IsEmpty(this.sumCell) || this.arrowCells.Any(i => grid.IsEmpty(i))) {
                return false;
            }

            return grid.Get(this.sumCell) == this.GetArrowSum(grid);
        }

        private int GetArrowSum(ISudokuGrid grid) =>
            this.arrowCells.Sum(i => grid.Get(i));

        private int GetMaxArrowSum(ISudokuGrid grid) =>
            this.arrowCells.Aggregate(0, (curr, next) => curr + (grid.IsEmpty(next) ? grid.Size : grid.Get(next)));

        private int GetMinArrowSum(ISudokuGrid grid) =>
            this.arrowCells.Aggregate(0, (curr, next) => curr + (grid.IsEmpty(next) ? 1 : grid.Get(next)));
    }
}
