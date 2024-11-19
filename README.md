# Sudoku Solver
A .NET Sudoku solver that uses a combination of logical deduction and brute force to solve any Sudoku puzzle. Try it out at https://sudoku.robinclaesson.com/

It first tries to apply a series of logical rules to fill as many numbers as possible until no change is made. For simpler puzzles these rules can sometimes be enough to solve the puzzles. See the logical deduction rules section for details on the used rules.

If the logical rules are not enough to solve the puzzle the solver goes into a depth first brute force. It fills out the possible values for each remaining cells, and then picks one value for a cell and restarts the logical part. If it reaches a dead end it goes back up one step and tries another value. Since it restarts the logical rules after each "guess" the depth of the search is likely to be lower than the remaining number of cells at the first iteration. 

## Logical deduction rules

* **Calculate candidates:** The basic of all other rules. Fill the candidates list for all empty cells in the grid. A candidate is a possible number for the cell that would not break any rules in the current state of the grid. 
* **Obvious singles:** Finds empty cells with only one candidate and sets the value. https://sudoku.com/sudoku-rules/obvious-singles/ 
* **Obvious pairs:** Finds pairs of empty cells in a block with only the same two candidates, and removes these candidates from other cells in the block. https://sudoku.com/sudoku-rules/obvious-pairs/
* **Last free cell:** Finds rows, columns and blocks with only one empty cell and sets the value. https://sudoku.com/sudoku-rules/hidden-singles/
* **Hidden singles:** Finds empty cells with a unique candidate in the row, column or block and sets the value. https://sudoku.com/sudoku-rules/hidden-singles/
* **Last possible number:** Finds cells where all other values are already in the row, column or block and sets the value. https://sudoku.com/sudoku-rules/last-possible-number/

## Applications
### Web App
The easiest way to test the solver is via the Blazor WebAssembly webb-app available at [https://sudoku.robinclaesson.com/](https://sudoku.robinclaesson.com/).

![Sudoku solver web app example run](/Assets/sudoku-web.gif)

### Forms App
There is also a desktop forms app. It does however for now still require internet for fetching the puzzles via the api. 
![Sudoku solver desktop app example run.](/Assets/sudoku-forms.gif)


### Core
.NET 8 class library for the solver and the supporting classes.

## Roadmap (Unordered and SoonTm)
* Manual entering of puzzles for Web and Forms
* Settings for not rendering every step in solving (Improves performance)
* Add more logical rules
* Add image recognition to import grid from picture
* Generate own puzzles
* Create SolveAsync() for solving asynchronously 
* Save every solved step for replay (With depth-first tree?)

## Credits - Puzzle generator API
The Sudoku puzzles solved by this program is being fetched from https://sudoku-api.vercel.app/ by [Marcus0086](https://github.com/Marcus0086).