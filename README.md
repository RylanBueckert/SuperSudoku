# Super Sudoku

This project solves multiple types of common variant or standard sudoku puzzles.

## Inputting Puzzles

This application takes .json files as input. The input is assumed to be correct, so if a file is missing any expected data or is not structured correctly,
it may die ungracefully. Extra fields should be ok! See the sample puzzles for examples.

### JSON Structure

The root JSON object has these fields:
- [Name](#Name-and-Author) (Optional)
- [Author](#Name-and-Author) (Optional)
- [Size](#Size)
- [GivenDigits](#Given-Digits)
- [Rules](#Rules)

#### Name and Author

Currently these fields are not used for anything, but I like to have them as a credit of who created the puzzle.

#### Size

The dimensions of the puzzle given as an integer. Essentially the number of cells in a row/column. Puzzles are assumed to be square. For standard sudoku puzzles, this is always 9.
Currently it is not possible to input puzzles with a size greater than 9.

#### Given Digits

The starting state of the puzzle. This is given as an array of strings. The array should have [size](#Size) strings, and each string should be [size](#Size)
characters long. Think of the grid of characters as the sudoku grid itself. Place digits between 1 and the puzzle's [size](#Size),
and use `.` (period) for cells that are empty. Here's an example ([World's Hardest Sudoku](https://www.conceptispuzzles.com/index.aspx?uri=info/article/424)):
```
"GivenDigits": [
    "8........",
    "..36.....",
    ".7..9.2..",
    ".5...7...",
    "....457..",
    "...1...3.",
    "..1....68",
    "..85..1..",
    ".9...4..."
  ]
```
  
#### Rules
  
These are all the rules for the puzzle. They are given as an array of objects that contain the rule name and any additional information (if required)
used to set up the rule. These vary between rules, so see the section on rules (TODO) for specific structure.  
Currently supported rules are:
- Standard
- Rows
- Columns
- Arrow
- Thermo
- Regions
- Diagonal
- Killer
- AntiKing
- AntiKnight
- Disjoint
- Palindrome
