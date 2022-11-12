//using System.Collections.Generic;

//using SuperSudoku.Sudoku.Grid;

//namespace SuperSudoku.Sudoku.Constraints
//{
//    public abstract class BaseSudukuConstraint : ISudokuConstraint
//    {
//        public abstract bool Validate(ISudokuGrid grid);

//        public virtual bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
//        {
//            if (!grid.IsEmpty()) {
//                return false;
//            }

//            if (this.AffectsCell(rowCol)) {
//                //if (grid.Set(row, col, value)) {
//                //    bool result = this.Validate(grid);
//                //    grid.Clear(row, col);
//                //    return result;
//                //}

//                return false;
//            }

//            return true;
//        }

//        public abstract bool AffectsCell(RowCol rowCol);

//        public abstract IEnumerable<RowCol> AffectedCells();
//    }
//}
