using System;
using System.Linq;

using SuperSudoku.Sudoku;
using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Solver
{
    public class SudokuSolver : ISudokuSolver
    {
        public bool Solve(ISudokuGrid grid)
        {
            RowCol? startCell = NextCell(grid);
            if (startCell.HasValue) {
                return Solve(grid, startCell.Value, true);
            }

            // No Empty cells, check if correct
            return grid.IsSolved();
        }

        private static bool Solve(ISudokuGrid grid, RowCol rowCol, bool print)
        {
            if (print) {
                Console.WriteLine(grid);
            }

            for (int i = 1; i <= grid.Size; i++) {
                if (grid.IsValid(rowCol, i)) {
                    grid.Set(rowCol, i);

                    RowCol? nextCell = NextCell(grid);
                    if (!nextCell.HasValue) {
                        // Puzzle Finished
                        return grid.IsSolved();
                    }

                    if (Solve(grid, nextCell.Value, print)) {
                        return true;
                    }

                    grid.Clear(rowCol);
                }
            }

            return false;
        }

        private static RowCol? NextCell(ISudokuGrid grid)
        {
            RowCol? bestCell = null;

            int bestRestriction = -1;
            int bestConstraints = 0;

            for (int row = 1; row <= grid.Size; row++) {
                for (RowCol currentCell = (row, 1); currentCell.Col <= grid.Size; currentCell = (row, currentCell.Col + 1)) {

                    if (grid.IsEmpty(currentCell)) {

                        int currentRestriction = 0;
                        for (int val = 1; val <= grid.Size; val++) {
                            if (!grid.IsValid(currentCell, val)) {
                                currentRestriction++;
                            }
                        }

                        if (currentRestriction >= bestRestriction) {
                            int currentConstraints = grid.Constraints().Sum(c => c.AffectsCell(currentCell) ? 1 : 0);

                            if (currentRestriction > bestRestriction || currentConstraints > bestConstraints) {
                                bestRestriction = currentRestriction;
                                bestConstraints = currentConstraints;
                                bestCell = currentCell;
                            }
                        }
                    }
                }
            }

            return bestCell;
        }
    }
}
