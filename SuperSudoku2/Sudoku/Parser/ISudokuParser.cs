using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Parser
{
    public interface ISudokuParser
    {
        ISudokuGrid Parse(string path);
    }
}
