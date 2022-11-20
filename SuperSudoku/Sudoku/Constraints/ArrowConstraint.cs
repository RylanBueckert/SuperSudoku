using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class ArrowConstraint : ISudokuConstraint
    {
        private readonly RowCol sumCell;
        private readonly HashSet<RowCol> arrowCells;

        public ArrowConstraint(RowCol sumCell, params RowCol[] arrowCells)
        {
            this.sumCell = sumCell;
            this.arrowCells = arrowCells.ToHashSet();
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol)
        {
            if (this.AffectsCell(rowCol)) {
                return this.arrowCells.Append(this.sumCell).Where(i => i != rowCol);
            }

            return Enumerable.Empty<RowCol>();
        }

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                if (rowCol == this.sumCell) {
                    return this.GetMaxArrowSum(grid) >= value && this.GetMinArrowSum(grid) <= value;
                }
                else {
                    int oldValue = grid.Get(rowCol);
                    if (!grid.Set(rowCol, value)) {
                        return false;
                    }

                    int min = this.GetMinArrowSum(grid);
                    int max = this.GetMaxArrowSum(grid);

                    if (oldValue == ISudokuGrid.EmptyValue) {
                        grid.Clear(rowCol);
                    }
                    else {
                        grid.Set(rowCol, oldValue);
                    }

                    if (grid.IsEmpty(this.sumCell)) {
                        return min <= grid.Size;
                    }
                    else {
                        int target = grid.Get(this.sumCell);
                        return min <= target && max >= target;
                    }
                }
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid)
        {
            if (grid.IsEmpty(this.sumCell) || this.arrowCells.Any(grid.IsEmpty)) {
                return false;
            }

            return grid.Get(this.sumCell) == this.GetArrowSum(grid);
        }

        private bool AffectsCell(RowCol rowCol) =>
            rowCol == this.sumCell || this.arrowCells.Contains(rowCol);

        private int GetArrowSum(ISudokuGrid grid) =>
            this.arrowCells.Sum(grid.Get);

        private int GetMaxArrowSum(ISudokuGrid grid) =>
            this.arrowCells.Aggregate(0, (curr, next) => curr + (grid.IsEmpty(next) ? grid.Size : grid.Get(next)));

        private int GetMinArrowSum(ISudokuGrid grid) =>
            this.arrowCells.Aggregate(0, (curr, next) => curr + (grid.IsEmpty(next) ? 1 : grid.Get(next)));
    }
}
