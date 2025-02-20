# Obstacle Robot #
At work we like to do code challenges sometimes, where someone will post a challenge problem, and everyone will try to code up the best or most creative solution.

The following problem was proposed:

## Code Challenge 6: Path planning ##

Even though I never "graded" all the previous code challenge submissions (thanks Mike and... uh just Mike (Mike wins! (ok, I submitted one, but I think Mike's is better))), here's a new challenge for your coding pleasure.

_Functional requirements_

Given an NxM grid, where each cell in the grid can be empty (0) or an obstacle (-1), find and display the shortest path between two arbitrary cells. The grid can be populated randomly with obstacles, or alternately a list of predefined obstacles can be placed in the grid. "Display the shortest path" can mean whatever you want it to mean: just dump the path coordinates, or plot the path on a grid, or whatever.

Assume you have an imaginary robot that always has a cardinal orientation N, E, S, W, and that can move forward by one cell, or rotate 90 degrees to the left or right. If the robot starts in an arbitrary cell with arbitrary orientation, find the shortest path from that robot to a different arbitrary cell (the ending orientation of the robot is unimportant). Display the path in the form of a list of RELATIVE direction commands: L (left), R (right), F (forward). This is probably easiest to implement using the path generated in step 1.

Given the path found in step 2, reverse the path.

_Options_

If you'd prefer to create a grid using the space character and the X character instead of 0 or -1, or any other substitute characters or numbers, go ahead and do that. Display the path and/or the grid any way you like, text or graphics. The important output is the path, not the map.

And I noticed that the functional requirements never specified that the robot needed to avoid the obstacles.  And we all know what the shortest distance between two points is...
