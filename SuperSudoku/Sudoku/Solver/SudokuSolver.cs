using System;
using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku;
using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Solver
{
    public class SudokuSolver : ISudokuSolver
    {
        public bool DoPrint { get; set; }

        public SudokuSolver(bool print = false)
        {
            this.DoPrint = print;
        }

        public bool Solve(ISudokuGrid grid)
        {
            if (this.DoPrint) {
                Console.WriteLine(grid);
            }

            (RowCol? rowCol, var candidates) = NextCell(grid);

            if (rowCol.HasValue) {

                foreach (int val in candidates) {
                    grid.Set(rowCol.Value, val);

                    if (Solve(grid)) {
                        return true;
                    }

                    grid.Clear(rowCol.Value);
                }
            }

            return grid.IsSolved();
        }

        private static (RowCol?, IEnumerable<int>) NextCell(ISudokuGrid grid)
        {
            RowCol? bestCell = null;
            IEnumerable<int> bestCellCandidates = null;

            int bestRestriction = grid.Size + 1;

            for (int row = 1; row <= grid.Size; row++) {
                for (RowCol currentCell = (row, 1); currentCell.Col <= grid.Size; currentCell = (row, currentCell.Col + 1)) {

                    if (grid.IsEmpty(currentCell)) {

                        List<int> currentCandidates = Enumerable.Range(1, grid.Size).Where(val => grid.IsValid(currentCell, val)).ToList();
                        int currentRestriction = currentCandidates.Count;

                        if (currentRestriction < bestRestriction) {
                            bestRestriction = currentRestriction;
                            bestCell = currentCell;
                            bestCellCandidates = currentCandidates;
                        }
                    }
                }
            }

            return (bestCell, bestCellCandidates);
        }
    }
}
