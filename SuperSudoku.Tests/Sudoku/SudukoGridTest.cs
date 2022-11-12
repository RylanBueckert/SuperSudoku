using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SuperSudoku.Sudoku;
using SuperSudoku.Sudoku.Constraints;

namespace SuperSudokuTest.Sudoku {
	[TestClass]
	public class SudokuGridTest {

		private readonly Random rand = new Random();

		[TestMethod]
		public void DefaultGridEmpty() {
			var sut = new SudokuGrid();

			for(int i = 1; i <= 9; i++) {
				for(int j = 1; j <= 9; j++) {
					Assert.IsTrue(sut.IsEmpty(i, j));
				}
			}
		}

		[TestMethod]
		public void DefaultGridConstraints() {
			var sut = new SudokuGrid();

			IReadOnlyCollection<ISudokuConstraint> constraints = sut.Constraints();

			Assert.AreEqual(3, constraints.Count);
			Assert.IsTrue(constraints.Where(c => c.GetType() == typeof(RowsConstraint)).Any());
			Assert.IsTrue(constraints.Where(c => c.GetType() == typeof(ColumnsConstraint)).Any());
			Assert.IsTrue(constraints.Where(c => c.GetType() == typeof(BoxsConstraint)).Any());
		}

		[TestMethod]
		public void Set_ValueSet() {
			int row = rand.Next(1, 10);
			int col = rand.Next(1, 10);
			int value = rand.Next(1, 10);
			var sut = new SudokuGrid();

			sut.Set(row, col, value);

			Assert.AreEqual(value, sut.Get(row, col));
		}

		[TestMethod]
		public void Set_OtherNotSet() {
			int row = rand.Next(1, 10);
			int col = rand.Next(1, 10);
			int value = rand.Next(1, 10);
			var sut = new SudokuGrid();

			sut.Set(row, col, value);

			for(int i = 1; i <= 9; i++) {
				for(int j = 1; j <= 9; j++) {
					if(i == row && j == col)
						continue;
					Assert.IsTrue(sut.IsEmpty(i, j));
				}
			}
		}

		[DataTestMethod]
		[DataRow(0, 0)]
		[DataRow(0, 10)]
		[DataRow(10, 0)]
		[DataRow(10, 10)]
		[ExpectedException(typeof(ArgumentException))]
		public void Set_OutOfBounds_ThrowsExeption(int row, int col) {
			var sut = new SudokuGrid();

			sut.Set(row, col, 5);
		}

		[DataTestMethod]
		[DataRow(0, 0)]
		[DataRow(0, 10)]
		[DataRow(10, 0)]
		[DataRow(10, 10)]
		[ExpectedException(typeof(ArgumentException))]
		public void Get_OutOfBounds_ThrowsExeption(int row, int col) {
			var sut = new SudokuGrid();

			sut.Get(row, col);
		}
	}
}
