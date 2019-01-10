# ProceduralDungeons
An asset that procedurally generates dungeons in Unity3D

![alt text](Screenshots/maze1.JPG "Screenshot")

# Quick Start

Two sample scenes are included, one that builds the maze out of tiles and one that builds the maze out of room prefabs. DunGen.cs generates the layout of the dungeon and maze, and DunGenPlacer.cs places the actual tiles around the map. Simply press the play button in either example scene and a dungeon will be generated.

![alt text](Screenshots/maze2.JPG "Screenshot")
![alt text](Screenshots/maze.JPG "Screenshot")

# Features

Besides procedurally and building generating mazes/dungeons out of either tiles or prefabs, the scripts can also fill the generated dungeons with random objects. (Currently only supported for tile based dungeons)

# Options

![alt text](Screenshots/options.JPG "Screenshot")

All scripts have many options exposed to the inspector so lots of customization is possible.

# Generating from code

In order to build a dungeon from code, simply call the BuildDungeon() function from either the DunGenPlacerTile or DunGenPlacerPrefab script. The full function signature is below:

public void BuildDungeon(int mapRows, int mapColumns, int minCells = 10, int numRooms = 1, int startX = 1, int startY = 1, bool printMap = false)

![alt text](Screenshots/maze3.JPG "Screenshot")

# Acknowledments

Big thanks to Richard Hawkes for his videos on procedural generation, and Quaternius for his amazing free 3D assets.
