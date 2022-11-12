using System.Collections.Generic;

using SuperSudoku.Sudoku.Constraints;

namespace SuperSudoku.Sudoku.Grid
{
    public interface ISudokuGrid
    {
        int Size { get; }

        int Get(int row, int col);

        void Set(int row, int col, int value);

        void Clear();

        void Clear(int row, int col);

        bool IsEmpty(int row, int col);

        bool IsValid();

        bool IsValid(int row, int col, int value);

        bool IsSolved();

        void AddConstraint(ISudokuConstraint constraint);

        IReadOnlyCollection<ISudokuConstraint> Constraints();
    }
}
