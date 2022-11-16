using System.Collections.Generic;

namespace SuperSudoku.Sudoku.Constraints
{
    public class AntiKingConstraint : BasePatternConstraint, ISudokuConstraint
    {
        public AntiKingConstraint(int gridSize)
            : base(gridSize)
        {
        }

        protected override IEnumerable<RowCol> GenerateCellPattern(RowCol rowCol)
        {
            var result = new List<RowCol>();

            for (int row = rowCol.Row - 1; row <= rowCol.Row + 1; row++) {
                for (int col = rowCol.Col - 1; col <= rowCol.Col + 1; col++) {
                    if ((row, col) == rowCol) {
                        continue;
                    }

                    result.Add((row, col));
                }
            }

            return result;
        }
    }
}
