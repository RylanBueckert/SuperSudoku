using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Sudoku.Constraints
{
    public class DisjointConstraint : ISudokuConstraint
    {
        private readonly List<RegionConstraint> regions;

        public DisjointConstraint(int gridSize)
        {
            this.regions = new List<RegionConstraint>();

            var boxes = BoxesHelper.GetBoxes(gridSize);

            for (int positionInBox = 0; positionInBox < gridSize; positionInBox++) {
                this.regions.Add(new RegionConstraint(boxes.Select(box => box[positionInBox])));
            }
        }

        public IEnumerable<RowCol> AffectedCells(RowCol rowCol) =>
            this.regions.SelectMany(i => i.AffectedCells(rowCol)).Distinct();

        public bool IsValidPlacement(ISudokuGrid grid, RowCol rowCol, int value) =>
            this.regions.All(i => i.IsValidPlacement(grid, rowCol, value));

        public bool Validate(ISudokuGrid grid) =>
            this.regions.All(i => i.Validate(grid));
    }
}
