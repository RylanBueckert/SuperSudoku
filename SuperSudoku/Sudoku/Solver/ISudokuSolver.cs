using SuperSudoku.Sudoku;

namespace SuperSudoku.Solver
{
    public interface ISudokuSolver
    {
        bool Solve(SudokuGrid grid);
    }
}