using System;
using System.Diagnostics;
using System.Windows.Forms;

using SuperSudoku.Parser;
using SuperSudoku.Solver;
using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            string filepath;

            using (OpenFileDialog dlg = new OpenFileDialog()) {
                dlg.Filter = "JSON files (*.json)|*.json";
                if (dlg.ShowDialog() == DialogResult.OK) {
                    filepath = dlg.FileName;
                }
                else {
                    return;
                }
            }

            ISudokuParser parser = new SudokuJsonParser();

            ISudokuGrid grid = parser.Parse(filepath);

            ISudokuSolver solver = new SudokuSolver();
            Console.WriteLine(grid);
            TimeSpan duration = Time(() => solver.Solve(grid));
            Console.WriteLine(grid);
            Console.WriteLine(duration.TotalMilliseconds);
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
