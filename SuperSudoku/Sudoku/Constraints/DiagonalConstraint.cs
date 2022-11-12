using System.Collections;

namespace SuperSudoku.Sudoku.Constraints {
	public class DiagonalConstraintTLBR : BaseSudukuConstraint, ISudokuConstraint {
		public override bool Validate(ISudokuGrid grid) {
			BitArray used = new BitArray(grid.Size + 1);

			for (int index = 1; index <= grid.Size; index++) {

				if (!grid.IsEmpty(index, index)) {
					if (used[grid.Get(index, index)]) {
						return false;
					} else {
						used[grid.Get(index, index)] = true;
					}
				}
			}

			return true;
		}

		public override bool IsValid(ISudokuGrid grid, int row, int col, int value) {
			if (!this.AffectsCell(grid, row, col)) {
				return true;
			}

			for (int i = 1; i <= grid.Size; i++) {
				if (i != row && grid.Get(i, i) == value) {
					return false;
				}
			}

			return true;
		}

		public override bool AffectsCell(ISudokuGrid grid, int row, int col) =>
			row == col;
	}

	public class DiagonalConstraintBLTR : ISudokuConstraint {
		public bool Validate(ISudokuGrid grid) {
			BitArray used = new BitArray(grid.Size + 1);

			for (int row = 1; row <= grid.Size; row++) {

				int col = grid.Size - row + 1;

				if (!grid.IsEmpty(row, col)) {
					if (used[grid.Get(row, col)]) {
						return false;
					} else {
						used[grid.Get(row, col)] = true;
					}
				}
			}

			return true;
		}

		public bool IsValid(ISudokuGrid grid, int row, int col, int value) {
			if (!this.AffectsCell(grid, row, col)) {
				return true;
			}

			for (int i = 1; i <= grid.Size; i++) {
				if (i != row && grid.Get(i, grid.Size - i + 1) == value) {
					return false;
				}
			}

			return true;
		}

		public bool AffectsCell(ISudokuGrid grid, int row, int col) =>
			row == grid.Size - col + 1;
	}
}
