using System.Text;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Formatter
{
    public static class SudokuFormatter
    {
        public static string Format(ISudokuGrid grid, int rowsInBox, int colsInBox)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 1; i <= grid.Size; i++) {
                for (int j = 1; j <= grid.Size; j++) {
                    RowCol rowCol = (i, j);

                    stringBuilder.Append(grid.IsEmpty(rowCol) ? " " : grid.Get(rowCol).ToString());

                    if (j % colsInBox == 0 && j != grid.Size) {
                        stringBuilder.Append('║');
                    }
                }
                stringBuilder.Append('\n');

                if (i % rowsInBox == 0 && i != grid.Size) {
                    for (int k = 1; k <= grid.Size; k++) {
                        stringBuilder.Append('=');

                        if (k % colsInBox == 0 && k != grid.Size) {
                            stringBuilder.Append('╬');
                        }
                    }
                    stringBuilder.Append('\n');
                }

            }

            return stringBuilder.ToString();
        }
    }
}
