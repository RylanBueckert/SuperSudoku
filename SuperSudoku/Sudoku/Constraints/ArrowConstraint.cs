using System;
using System.Collections.Generic;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints {
	public class ArrowConstraint : BaseSudukuConstraint, ISudokuConstraint {
		public ArrowConstraint() {
			this.arrowCells = new List<Tuple<int, int>>();
		}

		public override bool Validate(ISudokuGrid grid) {
			if (this.sumCell == null) {
				throw new InvalidOperationException("The sum cell has not been set");
			}

			bool arrowComplete = true;
			int sum = 0;
			foreach (var cell in this.arrowCells) {
				if (grid.IsEmpty(cell.Item1, cell.Item2)) {
					arrowComplete = false;
				} else {
					sum += grid.Get(cell.Item1, cell.Item2);
				}
			}

			if (grid.IsEmpty(this.sumCell.Item1, this.sumCell.Item2)) {
				return sum <= grid.Size;
			} else {
				return arrowComplete ? grid.Get(this.sumCell.Item1, this.sumCell.Item2) == sum : grid.Get(this.sumCell.Item1, this.sumCell.Item2) > sum;
			}
		}

		public override bool AffectsCell(ISudokuGrid grid, int row, int col) {
			Tuple<int, int> cell = new Tuple<int, int>(row, col);
			return this.sumCell.Equals(cell) || this.arrowCells.Contains(cell);
		}

		public void SetSumCell(int row, int col) =>
			this.sumCell = new Tuple<int, int>(row, col);

		public void AddArrowCell(int row, int col) =>
			this.arrowCells.Add(new Tuple<int, int>(row, col));

		private readonly List<Tuple<int, int>> arrowCells;

		private Tuple<int, int> sumCell = null;
	}
}
