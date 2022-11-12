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
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"File {path} could not be found");
            }
            JObject json = JObject.Parse(File.ReadAllText(path));

            int size = json.Value<int>("Size");
            string[] given = json["GivenDigits"].Children().Select(i => i.Value<string>()).ToArray();
            IEnumerable<JToken> rules = json["Rules"].Children();

            SudokuGrid sudokuGrid = new SudokuGrid(size);

            // Grid starting digits
            if (given.Length != sudokuGrid.Size || given.Any(i => i.Length != sudokuGrid.Size)) {
                throw new InvalidDataException("Given digits are not in a valid format");
            }

            for (int i = 1; i <= sudokuGrid.Size; i++) {
                for (int j = 1; j <= sudokuGrid.Size; j++) {
                    char c = given[i - 1][j - 1];

                    if (!(char.IsDigit(c) || c == '.')) {
                        throw new InvalidDataException($"{c} is not a valid digit");
                    }

                    if (char.IsDigit(c)) {
                        sudokuGrid.Set((i, j), int.Parse(c.ToString()));
                    }
                }
            }

            // Setup rules
            rules.ForEach(rule => {
                string ruleName = rule.Value<string>("Name");

                switch (ruleName.ToUpperInvariant()) {
                    case "STANDARD":
                        sudokuGrid.AddNormalSudokuConstraints();
                        break;

                    case "ARROW":
                        rule["Data"].Children().ForEach(arrowRule => {
                            RowCol sum = arrowRule["Sum"].Values<int>().ToArray().Map(i => (RowCol)(i[0], i[1]));
                            IEnumerable<RowCol> arrow = arrowRule["Arrow"].Select(i => i.Values<int>().ToArray().Map(j => (RowCol)(j[0], j[1])));

                            sudokuGrid.AddConstraint(new ArrowConstraint(sum, arrow));
                        });
                        break;

                    default:
                        throw new InvalidDataException($"Unknown rule: {ruleName}");
                }
            });

            return sudokuGrid;
        }
    }
}
