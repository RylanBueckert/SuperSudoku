using System.Collections.Generic;

namespace SuperSudoku.Sudoku.Constraints
{
    public class AntiKnightConstraint : BasePatternConstraint, ISudokuConstraint
    {
        public AntiKnightConstraint(int gridSize)
            : base(gridSize)
        {
        }

        protected override IEnumerable<RowCol> GenerateCellPattern(RowCol rowCol) =>
            new List<RowCol>
            {
                (rowCol.Row - 2, rowCol.Col - 1),
                (rowCol.Row - 2, rowCol.Col + 1),
                (rowCol.Row - 1, rowCol.Col - 2),
                (rowCol.Row - 1, rowCol.Col + 2),
                (rowCol.Row + 1, rowCol.Col - 2),
                (rowCol.Row + 1, rowCol.Col + 2),
                (rowCol.Row + 2, rowCol.Col - 1),
                (rowCol.Row + 2, rowCol.Col + 1)
            };
    }
}
