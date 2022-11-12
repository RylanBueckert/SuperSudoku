using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Solver
{
    public interface ISudokuSolver
    {
        bool Solve(ISudokuGrid grid);
    }
}