using System.Collections.Generic;

using SuperSudoku.Sudoku.Constraints;

namespace SuperSudoku.Sudoku.Grid
{
    public interface ISudokuGrid
    {
        int Size { get; }

        int Get(RowCol rowCol);

        bool Set(RowCol rowCol, int value, bool isGiven = false);

        void Clear(bool clearGiven = false);

        bool Clear(RowCol rowCol, bool clearGiven = false);

        bool IsEmpty(RowCol rowCol);

        bool IsGiven(RowCol rowCol);

        bool IsSolved();

        bool IsValid(RowCol rowCol, int value);

        void AddConstraint(ISudokuConstraint constraint);

        IEnumerable<ISudokuConstraint> Constraints();
    }
}
