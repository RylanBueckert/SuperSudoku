using System;
using System.Diagnostics;

using SuperSudoku.Parser;
using SuperSudoku.Solver;
using SuperSudoku.Sudoku;
using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku
{
    class Program
    {
        public static object SudokuParser { get; private set; }

        static void Main()
        {
            //SudokuGrid x = new SudokuGrid();
            //for (int i = 1; i <= x.Size; i++) {
            //	for (int j = 1; j <= x.Size; j++) {
            //		x.Set(i, j, 5);
            //	}
            //}
            //		Console.WriteLine(x);

            OpenFileDialog

            //string filename = Console.ReadLine();
            //SudokuGrid grid = SudokuParser.Parse(filename);
            //ISudokuSolver solver = new SudokuSolver();
            //Console.WriteLine(grid);
            //TimeSpan duration = Time(() => solver.Solve(grid));
            //Console.WriteLine(grid);
            //Console.WriteLine(duration.TotalMilliseconds);
        }

        public static TimeSpan Time(Func<bool> action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
