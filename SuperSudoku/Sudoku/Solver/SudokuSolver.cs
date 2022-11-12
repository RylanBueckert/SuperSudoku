using System;
using System.Linq;

using SuperSudoku.Sudoku;

namespace SuperSudoku.Solver {
    public class SudokuSolver : ISudokuSolver
    {
        public bool Solve(SudokuGrid grid)
        {
            int col;
            //if (NextCell(grid, out int row, out col)) {
            //    return Solve(grid, row, col);
            //}

            return true;
        }

        private static bool Solve(SudokuGrid grid, int row, int col)
        {
            Console.WriteLine(grid);

            //for (int i = 1; i <= grid.Size; i++) {
            //    if (grid.IsValid(row, col, i)) {
            //        grid.Set(row, col, i);

            //        if (!NextCell(grid, out int nextRow, out int nextCol)) {
            //            // Puzzle Solved
            //            return true;
            //        }

            //        if (Solve(grid, nextRow, nextCol)) {
            //            return true;
            //        }

            //        grid.Clear(row, col);
            //    }
            //}

            return false;
        }

        //private static bool NextCell(SudokuGrid grid, out int row, out int col)
        //{
        //    //row = 0;
        //    //col = 0;

        //    //int bestRestriction = -1;
        //    //int bestConstraints = 0;

        //    //for (int i = 1; i <= grid.Size; i++) {
        //    //    for (int j = 1; j <= grid.Size; j++) {

        //    //        if (grid.IsEmpty(i, j)) {

        //    //            int currentRestriction = 0;
        //    //            for (int val = 1; val <= grid.Size; val++) {
        //    //                if (!grid.IsValid(i, j, val)) {
        //    //                    currentRestriction++;
        //    //                }
        //    //            }

        //    //            if (currentRestriction >= bestRestriction) {
        //    //                int currentConstraints = grid.Constraints().Aggregate(0, (sum, c) => sum += c.AffectsCell(grid, i, j) ? 1 : 0);

        //    //                if (currentRestriction > bestRestriction || currentConstraints > bestConstraints) {
        //    //                    bestRestriction = currentRestriction;
        //    //                    bestConstraints = currentConstraints;
        //    //                    row = i;
        //    //                    col = j;
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    //return bestRestriction > -1;
        //}
    }
}
