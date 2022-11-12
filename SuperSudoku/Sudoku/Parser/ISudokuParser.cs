using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Parser
{
    public interface ISudokuParser
    {
        SudokuGrid Parse(string path);
    }
}
