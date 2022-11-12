namespace SuperSudoku.Sudoku
{
    public class RowCol
    {
        public int Row { get; }

        public int Col { get; }

        public RowCol(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public override string ToString()
        {
            return $"({this.Row}, {this.Col})";
        }

        public override bool Equals(object obj) =>
            obj is RowCol && this.Equals((RowCol)obj);

        public bool Equals(RowCol other) =>
            this.Row == other.Row && this.Col == other.Col;

        public override int GetHashCode() =>
            this.Row.GetHashCode() * this.Col.GetHashCode();

        public static implicit operator RowCol((int, int) tuple) =>
            new RowCol(tuple.Item1, tuple.Item2);

        public static implicit operator (int, int)(RowCol rowCol) =>
            (rowCol.Row, rowCol.Col);

        public static bool operator ==(RowCol a, RowCol b) =>
            a.Equals(b);

        public static bool operator !=(RowCol a, RowCol b) =>
            !a.Equals(b);
    }
}
