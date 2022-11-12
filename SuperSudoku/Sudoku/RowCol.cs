namespace SuperSudoku.Sudoku
{
    public class RowCol
    {
        public int Row { get; set; }

        public int Col { get; set; }

        public override string ToString()
        {
            return $"({this.Row}, {this.Col})";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType())) {
                return false;
            }

            RowCol rowCol = (RowCol)obj;
            return this.Row == rowCol.Row && this.Col == rowCol.Col;
        }

        public static implicit operator RowCol((int, int) tuple) =>
            new RowCol { Row = tuple.Item1, Col = tuple.Item2 };

        public static implicit operator (int, int)(RowCol rowCol) =>
            (rowCol.Row, rowCol.Col);

        public static bool operator ==(RowCol a, RowCol b) =>
            a.Equals(b);

        public static bool operator !=(RowCol a, RowCol b) =>
            !(a == b);
    }
}
