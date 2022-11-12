using System.Collections.Generic;

using SuperSudoku.Sudoku.Constraints;

namespace SuperSudoku.Sudoku.Grid
{
    public interface ISudokuGrid
    {
        int Size { get; }

        int Get(RowCol rowCol);

        void Set(RowCol rowCol, int value);

        void Clear();

        void Clear(RowCol rowCol);

        bool IsEmpty(RowCol rowCol);

        bool IsSolved();

        bool IsValid(RowCol rowCol, int value);

        void AddConstraint(ISudokuConstraint constraint);

        IEnumerable<ISudokuConstraint> Constraints();
    }
}
