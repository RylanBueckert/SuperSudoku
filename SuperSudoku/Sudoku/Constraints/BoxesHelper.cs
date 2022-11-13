using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Utliity;

namespace SuperSudoku.Sudoku.Constraints
{
    public static class BoxesHelper
    {
        private readonly record struct BoxConfig(int boxRows, int boxColumns, int rowsInBox, int columnsInBox);
        private static readonly BoxConfig config9x9 = new BoxConfig(3, 3, 3, 3);
        private static readonly BoxConfig config6x6 = new BoxConfig(3, 2, 2, 3);
        private static readonly BoxConfig config4x4 = new BoxConfig(2, 2, 2, 2);
        private static readonly BoxConfig configDefault = new BoxConfig(0, 0, 0, 0);


        public static List<List<RowCol>> GetBoxes(int gridSize) =>
            GenerateBoxConstraints(GetBoxConfig(gridSize));

        public static int GetBoxNum(int gridSize, RowCol rowCol) =>
            GetBoxConfig(gridSize).Map(config =>
                (rowCol.Row - 1) / config.rowsInBox * config.boxColumns + (rowCol.Col - 1) / config.columnsInBox);

        private static BoxConfig GetBoxConfig(int gridSize)
        {
            switch (gridSize) {
                case 9:
                    return config9x9;
                case 6:
                    return config6x6;
                case 4:
                    return config4x4;
                default:
                    return configDefault;
            }
        }

        private static List<List<RowCol>> GenerateBoxConstraints(BoxConfig config) =>
                Enumerable.Range(0, config.boxRows).SelectMany(i => Enumerable.Range(0, config.boxColumns).Select(j =>
                    Enumerable.Range(1, config.rowsInBox).SelectMany(row => Enumerable.Range(1, config.columnsInBox).Select(col =>
                        new RowCol(i * config.rowsInBox + row, j * config.columnsInBox + col))).ToList())).ToList();



    }
}
