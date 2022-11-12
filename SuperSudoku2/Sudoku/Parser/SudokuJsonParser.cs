using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json.Linq;

using SuperSudoku.Sudoku.Constraints;
using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Parser
{

    public class SudokuJsonParser : ISudokuParser
    {
        public ISudokuGrid Parse(string path)
        {
            if (File.Exists(path)) {
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
                            sudokuGrid.Set(i, j, int.Parse(c.ToString()));
                        }
                    }
                }

                // Setup rules
                foreach (var rule in rules) {
                    string ruleName = rule.Value<string>("Name");

                    switch (ruleName.ToUpperInvariant()) {
                        case "STANDARD":
                            sudokuGrid.AddConstraint(new RowsConstraint());
                            sudokuGrid.AddConstraint(new ColumnsConstraint());

                            if (sudokuGrid.Size == 9) {
                                sudokuGrid.AddConstraint(new StandardBoxesConstraint());
                            }
                            break;

                        case "ARROW":
                            foreach(var arrow in rule["Data"].Children()) {

                            }
                            break;

                        default:
                            throw new InvalidDataException($"Unknown rule: {ruleName}");
                    }
                }

                //bool standardRules = sr.GetNextLine().ToUpperInvariant().First() == 'T';



                //while (!sr.EndOfStream) {
                //    string option = sr.GetWord()?.ToUpperInvariant();
                //    if (sr.EndOfStream) {
                //        break;
                //    }

                //    switch (option) {
                //        case "ARROW":
                //            ArrowConstraint arrow = new ArrowConstraint();
                //            string[] strCoords = sr.GetNextLine().Split(',');

                //            Tuple<int, int>[] coords = new Tuple<int, int>[strCoords.Length];

                //            for (int i = 0; i < strCoords.Length; i++) {
                //                StreamReader x = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(strCoords[i])));
                //                int first = int.Parse(x.GetWord());
                //                int second = int.Parse(x.GetWord());
                //                coords[i] = new Tuple<int, int>(first, second);
                //            }

                //            arrow.SetSumCell(coords[0].Item1, coords[0].Item2);

                //            for (int i = 1; i < coords.Length; i++) {
                //                arrow.AddArrowCell(coords[i].Item1, coords[i].Item2);
                //            }

                //            sudokuGrid.AddConstraint(arrow);
                //            break;

                //        case "DIAGONAL":
                //            string direction = sr.GetNextLine();
                //            if (direction.ToUpperInvariant() == "TLBR") {
                //                sudokuGrid.AddConstraint(new DiagonalConstraintTLBR());
                //            }
                //            else if (direction.ToUpperInvariant() == "BLTR") {
                //                sudokuGrid.AddConstraint(new DiagonalConstraintBLTR());
                //            }
                //            else {
                //                throw new InvalidDataException($"Invalid option format: {option} : {direction}");
                //            }
                //            break;


                //        default:
                //            throw new InvalidDataException($"Invalid puzzle option: {option}");
                //    }
                //}

                //return sudokuGrid;
            }

            throw new FileNotFoundException($"File {path} could not be found");
        }

        //private static string GetNextLine(this StreamReader sr)
        //{
        //    while (!sr.EndOfStream && char.IsWhiteSpace((char)sr.Peek())) {
        //        sr.Read();
        //    }
        //    return sr.ReadLine();
        //}

        //private static string GetWord(this StreamReader sr)
        //{
        //    while (!sr.EndOfStream && char.IsWhiteSpace((char)sr.Peek())) {
        //        sr.Read();
        //    }
        //    if (sr.EndOfStream) {
        //        return null;
        //    }

        //    string result = string.Empty;

        //    while (!sr.EndOfStream && !char.IsWhiteSpace((char)sr.Peek())) {
        //        result += (char)sr.Read();
        //    }

        //    return result;
        //}
    }
}
