namespace SuperSudoku.Sudoku
{
    public class RowCol
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public static implicit operator RowCol((int, int) tuple) =>
            new RowCol { Row = tuple.Item1, Col = tuple.Item2 };

        public static implicit operator (int, int)(RowCol rowCol) =>
            (rowCol.Row, rowCol.Col);

        public override string ToString()
        {
            return $"({this.Row}, {this.Col})";
        }
    }
}
