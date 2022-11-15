using System;
using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Constraints;
using SuperSudoku.Sudoku.Formatter;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Grid
{
    public class SudokuGrid : ISudokuGrid
    {
        public const int EMPTY = 0;
        private readonly int[,] grid;
        private readonly List<ISudokuConstraint> constraints;
        private readonly HashSet<RowCol> givenCells;

        public SudokuGrid(int size)
        {
            this.Size = size;
            this.grid = new int[this.Size, this.Size];
            this.constraints = new List<ISudokuConstraint>();
            this.givenCells = new HashSet<RowCol>();
        }

        public int Size { get; }

        public int Get(RowCol rowCol)
        {
            if (!this.IsValidRange(rowCol)) {
                throw new ArgumentOutOfRangeException(nameof(rowCol));
            }

            return this.At(rowCol);
        }

        public bool Set(RowCol rowCol, int value, bool setGiven = false)
        {
            if (!this.IsValidRange(rowCol)) {
                throw new ArgumentOutOfRangeException(nameof(rowCol));
            }

            if (!this.IsValidValue(value)) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (setGiven) {
                this.givenCells.Add(rowCol);
            }

            if (setGiven || !this.IsGiven(rowCol)) {
                this.At(rowCol) = value;
                return true;
            }

            return false;
        }

        public bool Clear(RowCol rowCol, bool clearGiven = false)
        {
            if (!this.IsValidRange(rowCol)) {
                throw new ArgumentOutOfRangeException(nameof(rowCol));
            }

            if (clearGiven) {
                this.givenCells.Remove(rowCol);
            }

            if (clearGiven || !this.IsGiven(rowCol)) {
                this.At(rowCol) = EMPTY;
                return true;
            }

            return false;
        }

        public void Clear(bool clearGiven = false)
        {
            if (clearGiven) {
                this.givenCells.Clear();
            }

            for (int row = 0; row < this.Size; row++) {
                for (int col = 0; col < this.Size; col++) {
                    this.At((row, col)) = EMPTY;
                }
            }
        }

        public bool IsEmpty(RowCol rowCol)
        {
            if (!this.IsValidRange(rowCol)) {
                throw new ArgumentOutOfRangeException(nameof(rowCol));
            }

            return this.At(rowCol) == EMPTY;
        }

        public bool IsGiven(RowCol rowCol) =>
            this.givenCells.Contains(rowCol);

        public bool IsSolved() =>
            this.grid.ToEnumerable().All(i => i != EMPTY) &&
            this.constraints.All(i => i.Validate(this));

        public bool IsValid(RowCol rowCol, int value) =>
            this.constraints.All(i => i.IsValidPlacement(this, rowCol, value));

        public void AddConstraint(ISudokuConstraint constraint) =>
            this.constraints.Add(constraint);

        public IEnumerable<ISudokuConstraint> Constraints() =>
            this.constraints;

        public override string ToString() =>
            SudokuFormatter.Format(this);

        private ref int At(RowCol rowCol) =>
            ref this.grid[rowCol.Row - 1, rowCol.Col - 1];

        private bool IsValidRange(RowCol rowCol) =>
            this.IsValidValue(rowCol.Row) &&
            this.IsValidValue(rowCol.Col);

        private bool IsValidValue(int val) =>
            val <= this.Size &&
            val > 0;
    }
}
