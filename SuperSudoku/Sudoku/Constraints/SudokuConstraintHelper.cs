using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Constraints
{
    public static class SudokuConstraintHelper
    {
        public static void AddNormalSudokuConstraints(this ISudokuGrid grid)
        {
            AddRowsConstraints(grid);
            AddColumnsConstraints(grid);
            AddStandardBoxConstraints(grid);
        }

        public static void AddRowsConstraints(this ISudokuGrid grid) =>
            Enumerable.Range(1, grid.Size)
                      .Select(row => new RegionConstraint(Enumerable.Range(1, grid.Size).Select(col => (RowCol)(row, col))))
                      .ForEach(grid.AddConstraint);

        public static void AddColumnsConstraints(ISudokuGrid grid) =>
            Enumerable.Range(1, grid.Size)
                      .Select(col => new RegionConstraint(Enumerable.Range(1, grid.Size).Select(row => (RowCol)(row, col))))
                      .ForEach(grid.AddConstraint);

        public static void AddStandardBoxConstraints(this ISudokuGrid grid)
        {
            switch (grid.Size) {
                case 9:
                    GenerateBoxConstraints(3, 3, 3, 3).ForEach(grid.AddConstraint);
                    break;
                case 6:
                    GenerateBoxConstraints(3, 2, 2, 3).ForEach(grid.AddConstraint);
                    break;
                case 4:
                    GenerateBoxConstraints(2, 2, 2, 2).ForEach(grid.AddConstraint);
                    break;
            }
        }

        public static void AddDiaganalTLBRConstraint(this ISudokuGrid grid) =>
            Enumerable.Range(1, grid.Size)
                      .Select(i => new RegionConstraint(Enumerable.Range(1, grid.Size).Select(_ => (RowCol)(i, i))))
                      .ForEach(grid.AddConstraint);

        public static void AddDiaganalBLTRConstraint(this ISudokuGrid grid) =>
            Enumerable.Range(1, grid.Size)
                      .Select(i => new RegionConstraint(Enumerable.Range(1, grid.Size).Select(_ => (RowCol)(grid.Size + 1 - i, i))))
                      .ForEach(grid.AddConstraint);

        private static IEnumerable<ISudokuConstraint> GenerateBoxConstraints(int boxRows, int boxColumns, int rowsInBox, int columnsInBox) =>
            Enumerable.Range(0, boxRows).SelectMany(i => Enumerable.Range(0, boxColumns).Select(j =>
                new RegionConstraint(Enumerable.Range(1, rowsInBox).SelectMany(row => Enumerable.Range(1, columnsInBox).Select(col =>
                    (RowCol)(i * rowsInBox + row, j * columnsInBox + col))))));
    }
}
