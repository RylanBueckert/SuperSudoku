using System.Collections.Generic;
using System.Linq;

namespace SuperSudoku.Sudoku
{
    public static class BoxesHelper
    {
        public readonly record struct BoxConfig(int boxRows, int boxColumns, int rowsInBox, int columnsInBox);

        public static readonly IReadOnlyDictionary<int, BoxConfig> boxConfigs = new Dictionary<int, BoxConfig>()
        {
            { 9, new BoxConfig(3, 3, 3, 3) },
            { 6, new BoxConfig(3, 2, 2, 3) },
            { 4, new BoxConfig(2, 2, 2, 2) }
        };

        public static bool HasBoxLayout(int gridSize) =>
            boxConfigs.ContainsKey(gridSize);

        public static List<List<RowCol>> GetBoxes(int gridSize)
        {
            BoxConfig? config = GetBoxConfig(gridSize);

            if (config.HasValue) {
                return GenerateBoxes(config.Value);
            }

            return new List<List<RowCol>>();
        }

        public static int GetBoxNum(int gridSize, RowCol rowCol)
        {
            BoxConfig? config = GetBoxConfig(gridSize);

            if (config.HasValue) {
                return (rowCol.Row - 1) / config.Value.rowsInBox * config.Value.boxColumns + (rowCol.Col - 1) / config.Value.columnsInBox;
            }

            return -1;
        }

        public static BoxConfig? GetBoxConfig(int gridSize)
        {
            if (boxConfigs.TryGetValue(gridSize, out BoxConfig config)) {
                return config;
            }

            return null;
        }

        private static List<List<RowCol>> GenerateBoxes(BoxConfig config) =>
                Enumerable.Range(0, config.boxRows).SelectMany(i => Enumerable.Range(0, config.boxColumns).Select(j =>
                    Enumerable.Range(1, config.rowsInBox).SelectMany(row => Enumerable.Range(1, config.columnsInBox).Select(col =>
                        new RowCol(i * config.rowsInBox + row, j * config.columnsInBox + col))).ToList())).ToList();



    }
}
