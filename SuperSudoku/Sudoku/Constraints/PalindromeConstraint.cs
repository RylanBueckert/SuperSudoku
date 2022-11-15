using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class PalindromeConstraint : ISudokuConstraint
    {
        private List<RowCol> palindrome;

        public PalindromeConstraint(params RowCol[] palindrome)
        {
            this.palindrome = palindrome.ToList();
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol)
        {
            if (this.AffectsCell(rowCol)) {
                return this.palindrome.Where(i => i != rowCol);
            }

            return Enumerable.Empty<RowCol>();
        }

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            int idx = this.palindrome.IndexOf(rowCol);
            if (idx >= 0) {
                int mirrorIdx = this.palindrome.Count - 1 - idx;
                if (!grid.IsEmpty(this.palindrome[mirrorIdx])) {
                    return grid.Get(this.palindrome[mirrorIdx]) == value;
                }
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid)
        {
            for (int idx = 0; idx < this.palindrome.Count / 2; idx++) {
                int mirrorIdx = this.palindrome.Count - 1 - idx;
                if (grid.Get(this.palindrome[idx]) != grid.Get(this.palindrome[mirrorIdx])) {
                    return false;
                }
            }

            return true;
        }

        private bool AffectsCell(RowCol rowCol) =>
            this.palindrome.Contains(rowCol);
    }
}
