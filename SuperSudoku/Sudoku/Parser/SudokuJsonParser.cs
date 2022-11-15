using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json.Linq;

using SuperSudoku.Sudoku;
using SuperSudoku.Sudoku.Constraints;
using SuperSudoku.Sudoku.Grid;
using SuperSudoku.Utliity;
using SuperSudoku.Utliity.Extentions;

namespace SuperSudoku.Parser
{

    public class SudokuJsonParser : ISudokuParser
    {
        public ISudokuGrid Parse(string path)
        {
            JObject json = ParseJson(path);
            int size = json.Value<int>("Size");
            SudokuGrid sudokuGrid = new SudokuGrid(size);
            SetGivenDigits(json, sudokuGrid);
            SetupRules(json, sudokuGrid);

            return sudokuGrid;
        }

        private static JObject ParseJson(string path)
        {
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"File {path} could not be found");
            }

            return JObject.Parse(File.ReadAllText(path));
        }

        private static void SetGivenDigits(JToken json, SudokuGrid sudokuGrid)
        {
            HandleGridArray(json["GivenDigits"], sudokuGrid, (row, col, c) => {
                if (!(char.IsDigit(c) || c == '.')) {
                    throw new InvalidDataException($"{c} is not a valid digit");
                }

                if (char.IsDigit(c)) {
                    sudokuGrid.Set((row, col), int.Parse(c.ToString()), true);
                }
            });
        }

        private static void SetupRules(JToken json, SudokuGrid sudokuGrid)
        {
            IEnumerable<JToken> rules = json["Rules"].Children();
            rules.ForEach(rule => {
                string ruleName = rule.Value<string>("Name");

                switch (ruleName.ToUpperInvariant()) {
                    case "STANDARD":
                        sudokuGrid.AddConstraint(new StandardConstraint(sudokuGrid.Size));
                        break;
                    case "ROWS":
                        sudokuGrid.AddConstraint(new RowsConstraint(sudokuGrid.Size));
                        break;
                    case "COLUMNS":
                        sudokuGrid.AddConstraint(new ColumnsConstraint(sudokuGrid.Size));
                        break;
                    case "ARROW":
                        HandleArrowRule(rule, sudokuGrid);
                        break;
                    case "THERMO":
                        HandleThermoRule(rule, sudokuGrid);
                        break;
                    case "REGIONS":
                        HandleRegionRule(rule, sudokuGrid);
                        break;
                    case "DIAGANAL":
                        HandleDiaganalRule(rule, sudokuGrid);
                        break;
                    case "KILLER":
                        HandleKillerRule(rule, sudokuGrid);
                        break;
                    case "ANTIKING":
                        sudokuGrid.AddConstraint(new AntiKingConstraint(sudokuGrid.Size));
                        break;
                    case "ANTIKNIGHT":
                        sudokuGrid.AddConstraint(new AntiKnightConstraint(sudokuGrid.Size));
                        break;
                    case "DISJOINT":
                        sudokuGrid.AddConstraint(new DisjointConstraint(sudokuGrid.Size));
                        break;
                    default:
                        throw new NotSupportedException($"Unknown rule: {ruleName}");
                }
            });
        }

        private static void HandleArrowRule(JToken rule, SudokuGrid sudokuGrid)
        {
            rule["Data"].Children().ForEach(arrowRule => {
                RowCol sum = arrowRule["Sum"].Values<int>().ToArray().Map(i => new RowCol(i[0], i[1]));
                IEnumerable<RowCol> arrow = arrowRule["Arrow"].Select(i => i.Values<int>().ToArray().Map(j => new RowCol(j[0], j[1])));

                sudokuGrid.AddConstraint(new ArrowConstraint(sum, arrow));
            });
        }

        private static void HandleThermoRule(JToken rule, SudokuGrid sudokuGrid)
        {
            rule["Data"].Children().ForEach(thermoRule => {
                RowCol[] thermo = thermoRule["Thermo"].Select(i => i.Values<int>().ToArray().Map(j => new RowCol(j[0], j[1]))).ToArray();

                sudokuGrid.AddConstraint(new ThermoConstraint(thermo));
            });
        }

        private static void HandleRegionRule(JToken rule, SudokuGrid sudokuGrid)
        {
            var regions = new Dictionary<char, List<RowCol>>();

            HandleGridArray(rule["Data"], sudokuGrid, (row, col, c) => {
                if (c != '.') {
                    if (regions.ContainsKey(c)) {
                        regions[c].Add((row, col));
                    }
                    else {
                        regions[c] = new List<RowCol>() { (row, col) };
                    }
                }
            });

            regions.ForEach(i => {
                sudokuGrid.AddConstraint(new RegionConstraint(i.Value));
            });
        }

        private static void HandleDiaganalRule(JToken rule, SudokuGrid sudokuGrid)
        {
            bool negativeDiag = rule["/"].Value<bool>();
            bool positiveDiag = rule["\\"].Value<bool>();

            sudokuGrid.AddConstraint(new DiagonalConstraint(sudokuGrid.Size, positiveDiag, negativeDiag));
        }

        private static void HandleKillerRule(JToken rule, SudokuGrid sudokuGrid)
        {
            var regions = new Dictionary<char, List<RowCol>>();

            HandleGridArray(rule["Cages"], sudokuGrid, (row, col, c) => {
                if (c != '.') {
                    if (regions.ContainsKey(c)) {
                        regions[c].Add((row, col));
                    }
                    else {
                        regions[c] = new List<RowCol>() { (row, col) };
                    }
                }
            });

            JToken sums = rule["Sums"];

            regions.ForEach(i => {
                int killerSum = sums[i.Key.ToString()].Value<int>();
                sudokuGrid.AddConstraint(new KillerConstraint(i.Value, killerSum));
            });
        }

        private static void HandleGridArray(JToken json, SudokuGrid sudokuGrid, Action<int, int, char> action)
        {
            string[] given = json.Children().Select(i => i.Value<string>()).ToArray();
            if (given.Length != sudokuGrid.Size || given.Any(i => i.Length != sudokuGrid.Size)) {
                throw new InvalidDataException();
            }

            for (int i = 0; i < sudokuGrid.Size; i++) {
                for (int j = 0; j < sudokuGrid.Size; j++) {
                    action(i + 1, j + 1, given[i][j]);
                }
            }
        }
    }
}
