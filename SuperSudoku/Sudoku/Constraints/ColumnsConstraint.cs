using System.Collections;

namespace SuperSudoku.Sudoku.Constraints {
	public class ColumnsConstraint : BaseSudukuConstraint, ISudokuConstraint {
		public override bool Validate(ISudokuGrid grid) {
			for (int col = 1; col <= grid.Size; col++) {
				
				BitArray used = new BitArray(grid.Size + 1);
				for (int row = 1; row <= grid.Size; row++) {
					
					if(!grid.IsEmpty(col, row)) {
						if(used[grid.Get(col, row)]) {
							return false;
						} else {
							used[grid.Get(col, row)] = true;
						}
					}
				}
			}
			return true;
		}

		public override bool IsValid(ISudokuGrid grid, int row, int col, int value) {
			for (int r = 1; r <= grid.Size; r++) {
				if (r != row && grid.Get(r, col) == value) {
					return false;
				}
			}

			return true;
		}
	}
}
