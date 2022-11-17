using System;
using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class GermanWhispersConstraint : ISudokuConstraint
    {
        private readonly List<RowCol> whispersLine;

        public GermanWhispersConstraint(params RowCol[] whispersLine)
        {
            this.whispersLine = whispersLine.ToList();
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol)
        {
            if (this.AffectsCell(rowCol)) {
                return this.whispersLine.Where(i => i != rowCol);
            }

            return Enumerable.Empty<RowCol>();
        }

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            int idx = this.whispersLine.IndexOf(rowCol);

            if (idx >= 0) {
                if (idx > 0 && !grid.IsEmpty(this.whispersLine[idx - 1]) &&
                    Math.Abs(grid.Get(this.whispersLine[idx - 1]) - value) < 5) {
                    return false;
                }

                if (idx < this.whispersLine.Count - 1 &&
                    !grid.IsEmpty(this.whispersLine[idx + 1]) &&
                    Math.Abs(grid.Get(this.whispersLine[idx + 1]) - value) < 5) {
                    return false;
                }
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid)
        {
            for (int i = 1; i < this.whispersLine.Count; i += 2) {
                if (Math.Abs(grid.Get(this.whispersLine[i - 1]) - grid.Get(this.whispersLine[i])) < 5) {
                    return false;
                }
            }

            if (this.whispersLine.Count % 2 == 1) {
                return !(Math.Abs(grid.Get(this.whispersLine[^2]) - grid.Get(this.whispersLine[^1])) < 5);
            }

            return true;
        }

        private bool AffectsCell(RowCol rowCol) =>
            this.whispersLine.Contains(rowCol);
    }
}
