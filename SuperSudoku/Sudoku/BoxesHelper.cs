using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Utliity;

namespace SuperSudoku.Sudoku
{
    public static class BoxesHelper
    {
        private readonly record struct BoxConfig(int boxRows, int boxColumns, int rowsInBox, int columnsInBox);

        private static readonly IReadOnlyDictionary<int, BoxConfig> boxConfigs = new Dictionary<int, BoxConfig>()
        {
            { 9, new BoxConfig(3, 3, 3, 3) },
            { 6, new BoxConfig(3, 2, 2, 3) },
            { 4, new BoxConfig(2, 2, 2, 2) }
        };

        public static bool HasBoxLayout(int gridSize) =>
            boxConfigs.ContainsKey(gridSize);

        public static List<List<RowCol>> GetBoxes(int gridSize) =>
            GenerateBoxConstraints(GetBoxConfig(gridSize));

        public static int GetBoxNum(int gridSize, RowCol rowCol) =>
            GetBoxConfig(gridSize).Map(config =>
                (rowCol.Row - 1) / config.rowsInBox * config.boxColumns + (rowCol.Col - 1) / config.columnsInBox);

        private static BoxConfig GetBoxConfig(int gridSize)
        {
            if (boxConfigs.TryGetValue(gridSize, out BoxConfig config))
            {
                return config;
            }

            // Unknown box layout, every cell is in its own box
            return new BoxConfig(gridSize, gridSize, 1, 1);
        }

        private static List<List<RowCol>> GenerateBoxConstraints(BoxConfig config) =>
                Enumerable.Range(0, config.boxRows).SelectMany(i => Enumerable.Range(0, config.boxColumns).Select(j =>
                    Enumerable.Range(1, config.rowsInBox).SelectMany(row => Enumerable.Range(1, config.columnsInBox).Select(col =>
                        new RowCol(i * config.rowsInBox + row, j * config.columnsInBox + col))).ToList())).ToList();



    }
}
