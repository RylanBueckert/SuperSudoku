using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class DiagonalConstraint : ISudokuConstraint
    {
        private readonly List<RegionConstraint> diagonals;

        public DiagonalConstraint(int gridSize, bool hasPositiveDiag, bool hasNegativeDiag)
        {
            this.diagonals = new List<RegionConstraint>();

            if (hasPositiveDiag) {
                this.diagonals.Add(new RegionConstraint(Enumerable.Range(1, gridSize).Select(i => new RowCol(i, i))));
            }

            if (hasNegativeDiag) {
                this.diagonals.Add(new RegionConstraint(Enumerable.Range(1, gridSize).Select(i => new RowCol(gridSize + 1 - i, i))));
            }
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol) =>
            this.diagonals.SelectMany(i => i.AffectedCells(rowCol)).Distinct();

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            this.diagonals.All(i => i.IsValidPlacement(grid, rowCol, value));

        public bool Validate(ISudokuGrid grid) =>
            this.diagonals.All(i => i.Validate(grid));
    }
}
