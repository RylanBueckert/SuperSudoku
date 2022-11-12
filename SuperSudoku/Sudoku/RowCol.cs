namespace SuperSudoku.Sudoku
{
    public readonly record struct RowCol(int Row, int Col)
    {
        public static implicit operator RowCol((int, int) tuple) =>
            new RowCol(tuple.Item1, tuple.Item2);

        public static implicit operator (int, int)(RowCol rowCol) =>
            (rowCol.Row, rowCol.Col);
    }
}
