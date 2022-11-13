using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class StandardConstraint : ISudokuConstraint
    {
        private readonly List<ISudokuConstraint> subConstraints;

        public StandardConstraint(int gridSize)
        {
            this.subConstraints = new List<ISudokuConstraint>
            {
                new RowsConstraint(gridSize),
                new ColumnsConstraint(gridSize)
            };

            if (BoxesHelper.HasBoxLayout(gridSize)) {
                this.subConstraints.Add(new BoxesConstraint(gridSize));
            }
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol) =>
            this.subConstraints.SelectMany(i => i.AffectedCells(rowCol)).Distinct();

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            this.subConstraints.All(i => i.IsValidPlacement(grid, rowCol, value));

        public bool Validate(ISudokuGrid grid)
        {
            return this.subConstraints.All(i => i.Validate(grid));
        }
    }
}
