# LiftControlSystem
Commmon dispatch algorithms 
1. First Come First Serve - Serve requests based on first come first serve basis.
2. Shortest Seek Time First - Serve requests based on the closed pickup floor.
3. SCAN - Continuosly scan all floors for any waiting picups and serve them.
4. LOOK - Its very similar to 'Shortest Seek Time First' but also looking ahead and see if there are
other requests to be served ahead.

To be more efficient, most of the implemenation combines several of above algorithms base on use cases.

This dispatch algorithm implemented in this program is  'Shortest Seek Time First' dispatch algorithm.

This program also implements Stratergy Pattern. 
If we add more algorithms, the control system can easily switch between the algorithms using the Stratergy Pattern.

## Environment
The application is a .Net core 3.1 project developed using C# in the visual studio 19 environment.

## How to run the application
There are 2 options to Run the project.
1. Run the program by opening the solution in visual studio 19 environment and press F5.
2. Run outside visual studio environment by copying the executables available in 'LiftControlSystem-Executable' folder of
the repository on your computer and run the "LiftControlSystem.exe"

## How to supply inputs
1. The program takes the number of passengers as input.
2. For each passenger it will asks the originating floor/level and the destination floor/level.

The number of lifts is currently set to 1 and the number of floors/levels is set to 10 + 1 (ground floor).
You may change the number of lifts by updating the "numberOfLifts" parameter in the Program.cs file.
You can also change the number of levels by modifying the "numberOfLevels" parameter in Program.cs file.
For every new "number of passengers" input, the lift controller starts fresh (i.e. all lifts starting from ground floor).
