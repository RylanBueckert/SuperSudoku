using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

using SuperSudoku.Sudoku.Constraints;
using SuperSudoku.Sudoku.Grid;

namespace SuperSudoku.Parser
{

    public class SudokuJsonParser : ISudokuParser
    {
        public static SudokuGrid Parse(string path)
        {
            //string fullPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{path}";
            JsonDocument doc = JsonDocument.Parse("");

            doc.RootElement.

            if (File.Exists(path)) {
                using (StreamReader sr = new StreamReader(File.OpenRead(path))) {
                    int size = int.Parse(sr.GetNextLine());

                    bool standardRules = sr.GetNextLine().ToUpperInvariant().First() == 'T';

                    SudokuGrid sudokuGrid = new SudokuGrid(size, standardRules);

                    // Grid starting digits
                    for (int i = 1; i <= sudokuGrid.Size; i++) {
                        for (int j = 1; j <= sudokuGrid.Size; j++) {
                            char c;
                            do {
                                c = (char)sr.Read();
                            } while (char.IsWhiteSpace(c));

                            if (!(char.IsDigit(c) || c == '.')) {
                                throw new InvalidDataException();
                            }

                            if (char.IsDigit(c)) {
                                sudokuGrid.Set(i, j, int.Parse(c.ToString()));
                            }
                        }
                    }

                    while (!sr.EndOfStream) {
                        string option = sr.GetWord()?.ToUpperInvariant();
                        if (sr.EndOfStream) {
                            break;
                        }

                        switch (option) {
                            case "ARROW":
                                ArrowConstraint arrow = new ArrowConstraint();
                                string[] strCoords = sr.GetNextLine().Split(',');

                                Tuple<int, int>[] coords = new Tuple<int, int>[strCoords.Length];

                                for (int i = 0; i < strCoords.Length; i++) {
                                    StreamReader x = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(strCoords[i])));
                                    int first = int.Parse(x.GetWord());
                                    int second = int.Parse(x.GetWord());
                                    coords[i] = new Tuple<int, int>(first, second);
                                }

                                arrow.SetSumCell(coords[0].Item1, coords[0].Item2);

                                for (int i = 1; i < coords.Length; i++) {
                                    arrow.AddArrowCell(coords[i].Item1, coords[i].Item2);
                                }

                                sudokuGrid.AddConstraint(arrow);
                                break;

                            case "DIAGONAL":
                                string direction = sr.GetNextLine();
                                if (direction.ToUpperInvariant() == "TLBR") {
                                    sudokuGrid.AddConstraint(new DiagonalConstraintTLBR());
                                }
                                else if (direction.ToUpperInvariant() == "BLTR") {
                                    sudokuGrid.AddConstraint(new DiagonalConstraintBLTR());
                                }
                                else {
                                    throw new InvalidDataException($"Invalid option format: {option} : {direction}");
                                }
                                break;


                            default:
                                throw new InvalidDataException($"Invalid puzzle option: {option}");
                        }
                    }

                    return sudokuGrid;
                }
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
