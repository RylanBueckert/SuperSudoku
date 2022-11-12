using System;
using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public abstract class BasePatternConstraint : ISudokuConstraint
    {
        private readonly int gridSize;

        protected BasePatternConstraint(int gridSize)
        {
            this.gridSize = gridSize;
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol) =>
            this.GenerateCellPattern(rowCol).Where(this.IsValid);

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            !this.AffectedCells(rowCol).Select(grid.Get).Contains(value);


        public bool Validate(ISudokuGrid grid)
        {
            for (int row = 1; row <= grid.Size; row++) {
                for (int col = 1; col <= grid.Size; col++) {
                    int val = grid.Get((row, col));
                    if (this.AffectedCells((row, col)).Select(grid.Get).Contains(val)) {
                        return false;
                    }
                }
            }

            return true;
        }

        protected abstract IEnumerable<RowCol> GenerateCellPattern(RowCol rowCol);

        private bool IsValid(RowCol rowCol) =>
            rowCol.Row > 0 && rowCol.Row <= this.gridSize &&
            rowCol.Col > 0 && rowCol.Col <= this.gridSize;
    }
}