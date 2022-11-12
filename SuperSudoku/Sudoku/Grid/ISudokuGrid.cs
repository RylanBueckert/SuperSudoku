using System.Collections.Generic;

using SuperSudoku.Sudoku.Constraints;

namespace SuperSudoku.Sudoku.Grid
{
    public interface ISudokuGrid
    {
        public int Size { get; }

        public int Get(int row, int col);

        public void Set(int row, int col, int value);

        public void Clear();

        public void Clear(int row, int col);

        public bool IsEmpty(int row, int col);

        public bool IsValid();

        public bool IsValid(int row, int col, int value);

        public bool IsSolved();

        public void AddConstraint(ISudokuConstraint constraint);

        public IReadOnlyCollection<ISudokuConstraint> Constraints();
    }
}
