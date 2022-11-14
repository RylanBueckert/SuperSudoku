using System;
using System.Collections.Generic;
using System.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class ThermoConstraint : ISudokuConstraint
    {
        private readonly List<RowCol> thermo;

        public ThermoConstraint(params RowCol[] thermo)
        {
            this.thermo = thermo.ToList();
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol)
        {
            if (this.AffectsCell(rowCol)) {
                return this.thermo;
            }

            return Enumerable.Empty<RowCol>();
        }

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value)
        {
            if (this.AffectsCell(rowCol)) {
                int thermoIdx = this.thermo.IndexOf(rowCol);

                if (value <= thermoIdx || value > grid.Size - (this.thermo.Count - 1 - thermoIdx)) {
                    return false;
                }

                // Check before
                int acceptable = value - 1;
                for (int i = thermoIdx - 1; i >= 0; i--) {
                    int val = grid.Get(this.thermo[i]);

                    if (val == SudokuGrid.EMPTY) {
                        acceptable--;
                        continue;
                    }
                    else if (val > acceptable) {
                        return false;
                    }
                    else {
                        break;
                    }
                }

                // Check After
                acceptable = value + 1;
                for (int i = thermoIdx + 1; i < this.thermo.Count; i++) {
                    int val = grid.Get(this.thermo[i]);

                    if (val == SudokuGrid.EMPTY) {
                        acceptable++;
                        continue;
                    }
                    else if (val < acceptable) {
                        return false;
                    }
                    else {
                        break;
                    }
                }
            }

            return true;
        }

        public bool Validate(ISudokuGrid grid)
        {
            if(grid.Get(this.thermo[0]) == SudokuGrid.EMPTY) {
                return false;
            }

            for (int i = 1; i < this.thermo.Count; i++) {
                if (grid.Get(this.thermo[i - 1]) >= grid.Get(this.thermo[i])) {
                    return false;
                }
            }

            return true;
        }

        private bool AffectsCell(RowCol rowCol) =>
            this.thermo.Contains(rowCol);
    }
}
