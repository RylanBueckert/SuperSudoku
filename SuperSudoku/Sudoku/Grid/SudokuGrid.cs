using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SuperSudoku.Sudoku.Constraints;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Sudoku.Grid
{

    public class SudokuGrid : ISudokuGrid
    {
        public int Size { get; }

        public SudokuGrid(int size)
        {
            this.Size = size;
            this.grid = new int[this.Size, this.Size];
            this.constraints = new List<ISudokuConstraint>();
        }

        public static SudokuGrid CreateDefault()
        {
            var newGrid = new SudokuGrid(9);
            newGrid.AddConstraint(new RowsConstraint());
            newGrid.AddConstraint(new ColumnsConstraint());
            newGrid.AddConstraint(new BoxsConstraint());
            return newGrid;
        }

        public int Get(int row, int col)
        {
            if (!this.isValidRange(row)) {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (!this.isValidRange(col)) {
                throw new ArgumentOutOfRangeException(nameof(col));
            }

            return grid[row - 1, col - 1];
        }

        public void Set(int row, int col, int value)
        {
            if (!this.isValidRange(row)) {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (!this.isValidRange(col)) {
                throw new ArgumentOutOfRangeException(nameof(col));
            }

            if (!this.isValidRange(value)) {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            if (grid[row - 1, col - 1] != EMPTY) {
                throw new InvalidOperationException("The cell is not empty");
            }

            grid[row - 1, col - 1] = value;
        }

        public void Clear(int row, int col)
        {
            if (!this.isValidRange(row)) {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (!this.isValidRange(col)) {
                throw new ArgumentOutOfRangeException(nameof(col));
            }

            grid[row - 1, col - 1] = EMPTY;
        }

        public void Clear()
        {
            for (int i = 0; i < this.Size; i++) {
                for (int j = 0; j < this.Size; j++) {
                    grid[i, j] = EMPTY;
                }
            }
        }

        public bool IsEmpty(int row, int col)
        {
            if (!this.isValidRange(row)) {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (!this.isValidRange(col)) {
                throw new ArgumentOutOfRangeException(nameof(col));
            }

            return grid[row - 1, col - 1] == EMPTY;
        }

        public bool IsValid() =>
            this.constraints.All(i => i.Validate(this));

        public bool IsValid(int row, int col, int value) =>
            this.constraints.All(i => i.IsValid(this, row, col, value));

        public bool IsSolved() =>
            this.grid.ToEnumerable().All(i => i != EMPTY) &&
            this.IsValid();

        public void AddConstraint(ISudokuConstraint constraint) =>
            this.constraints.Add(constraint);

        public IReadOnlyCollection<ISudokuConstraint> Constraints() =>
            constraints;

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(132);

            for (int i = 1; i <= this.Size; i++) {
                for (int j = 1; j <= this.Size; j++) {
                    int cellValue = this.Get(i, j);

                    stringBuilder.Append(cellValue > 0 ? cellValue.ToString() : " ");

                    if (j % 3 == 0 && j != this.Size) {
                        stringBuilder.Append('║');
                    }
                }
                stringBuilder.Append('\n');

                if (i % 3 == 0 && i != this.Size) {
                    for (int k = 1; k <= this.Size; k++) {
                        stringBuilder.Append('=');

                        if (k % 3 == 0 && k != this.Size) {
                            stringBuilder.Append('╬');
                        }
                    }
                    stringBuilder.Append('\n');
                }

            }

            return stringBuilder.ToString();
        }

        private const int EMPTY = 0;

        private int[,] grid;

        private List<ISudokuConstraint> constraints;

        private bool isValidRange(int value) =>
            value <= this.Size && value > 0;
    }
}
