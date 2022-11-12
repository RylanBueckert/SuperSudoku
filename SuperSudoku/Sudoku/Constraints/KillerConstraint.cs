using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class KillerConstraint : RegionConstraint, ISudokuConstraint
    {
        private readonly int sum;

        public KillerConstraint(IEnumerable<RowCol> cells, int sum)
            : base(cells)
        {
            this.sum = sum;
        }

        public override bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                if (!base.IsValidPlacement(grid, rowCol, value)) {
                    return false;
                }

                int oldValue = grid.Get(rowCol);
                if (!grid.Set(rowCol, value)) {
                    return false;
                }

                int min = this.GetMinPossibleSum(grid);
                int max = this.GetMaxPossibleSum(grid);

                if (oldValue == SudokuGrid.EMPTY) {
                    grid.Clear(rowCol);
                }
                else {
                    grid.Set(rowCol, oldValue);
                }

                return min <= this.sum && max >= this.sum;
            }

            return true;
        }

        public override bool Validate(ISudokuGrid grid) =>
            base.Validate(grid) && this.GetCurrentSum(grid) == this.sum;

        private int GetCurrentSum(ISudokuGrid grid) =>
            this.cells.Sum(grid.Get);

        private int GetMaxPossibleSum(ISudokuGrid grid)
        {
            var availble = Enumerable.Range(1, grid.Size).ToList();
            this.cells.Where(i => !grid.IsEmpty(i)).Select(grid.Get).Where(i => i != SudokuGrid.EMPTY).ForEach(i => availble.Remove(i));
            int idx = availble.Count - 1;

            return this.cells.Aggregate(0, (curr, next) => curr + (grid.IsEmpty(next) ? availble[idx--] : grid.Get(next)));
        }

        private int GetMinPossibleSum(ISudokuGrid grid)
        {
            var availble = Enumerable.Range(1, grid.Size).ToList();
            this.cells.Where(i => !grid.IsEmpty(i)).Select(grid.Get).Where(i => i != SudokuGrid.EMPTY).ForEach(i => availble.Remove(i));
            int idx = 0;

            return this.cells.Aggregate(0, (curr, next) => curr + (grid.IsEmpty(next) ? availble[idx++] : grid.Get(next)));
        }
    }
}
