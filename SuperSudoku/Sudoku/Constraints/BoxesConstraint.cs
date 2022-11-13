using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class BoxesConstraint : ISudokuConstraint
    {
        private readonly List<RegionConstraint> boxes;
        private readonly int gridSize;

        public BoxesConstraint(int gridSize)
        {
            this.boxes = BoxesHelper.GetBoxes(gridSize).Select(i => new RegionConstraint(i)).ToList();
            this.gridSize = gridSize;
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol) =>
            this.boxes[BoxesHelper.GetBoxNum(this.gridSize, rowCol)].AffectedCells(rowCol);

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            this.boxes[BoxesHelper.GetBoxNum(this.gridSize, rowCol)].IsValidPlacement(grid, rowCol, value);

        public bool Validate(ISudokuGrid grid) =>
            this.boxes.All(r => r.Validate(grid));
    }
}
