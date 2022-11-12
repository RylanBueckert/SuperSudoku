using System;
using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public class BoxesConstraint : ISudokuConstraint
    {
        private readonly List<RegionConstraint> boxes;

        public BoxesConstraint(int gridSize)
        {
            this.boxes = new List<RegionConstraint>();

            switch (gridSize) {
                case 9:
                    GenerateBoxConstraints(3, 3, 3, 3).ForEach(this.boxes.Add);
                    break;
                case 6:
                    GenerateBoxConstraints(3, 2, 2, 3).ForEach(this.boxes.Add);
                    break;
                case 4:
                    GenerateBoxConstraints(2, 2, 2, 2).ForEach(this.boxes.Add);
                    break;
            }
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol)
        {
            return this.boxes[GetBoxNum(rowCol)].AffectedCells(rowCol);
        }

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            this.boxes[GetBoxNum(rowCol)].IsValidPlacement(grid, rowCol, value);

        public bool Validate(ISudokuGrid grid) =>
            this.boxes.All(r => r.Validate(grid));

        private static IEnumerable<RegionConstraint> GenerateBoxConstraints(int boxRows, int boxColumns, int rowsInBox, int columnsInBox) =>
            Enumerable.Range(0, boxRows).SelectMany(i => Enumerable.Range(0, boxColumns).Select(j =>
                new RegionConstraint(Enumerable.Range(1, rowsInBox).SelectMany(row => Enumerable.Range(1, columnsInBox).Select(col =>
                    new RowCol(i * rowsInBox + row, j * columnsInBox + col))))));

        private static int GetBoxNum(RowCol rowCol) =>
            (rowCol.Row - 1) / 3 * 3 + (rowCol.Col - 1) / 3;
    }
}
