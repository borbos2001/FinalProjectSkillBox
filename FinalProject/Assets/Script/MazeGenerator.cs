
using System.Collections.Generic;

public class MazeGeneraorCell
{
    public int X;
    public int Y;

    public bool _leftSide = true;
    public bool _bottomSide = true;

    public bool Visited = false;
}
public class MazeGenerator 
{
    public int Width =13;
    public int Height = 13;
    public MazeGeneraorCell[,] GenerateNewMaze()
    {
        MazeGeneraorCell[,] _maze = new MazeGeneraorCell[Width, Height];
        for (int i = 0; i < _maze.GetLength(0); i++)
        {
            for(int j = 0; j < _maze.GetLength(1); j++)
            {
                _maze[i,j] = new MazeGeneraorCell { X = i , Y = j };
            }
        }

        RemoveWallsWithBackTracker(_maze);
        return _maze;
    }
    private void RemoveWallsWithBackTracker(MazeGeneraorCell[,] maze)
    {
        MazeGeneraorCell current = maze[1, 1];
        current.Visited = true;

        Stack<MazeGeneraorCell> stack = new Stack<MazeGeneraorCell> ();
        do
        {
            List<MazeGeneraorCell> unvisitedNeighbours = new List<MazeGeneraorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 1 && !maze[x- 2, y].Visited) unvisitedNeighbours.Add(maze[x - 2,y ]);
            if (y > 1 && !maze[x,y - 2].Visited) unvisitedNeighbours.Add (maze[x,y -2]);
            if (x < Width - 2 && !maze[x + 2, y].Visited) unvisitedNeighbours.Add(maze[x + 2, y]);
            if (y < Height - 2 && !maze[x, y + 2].Visited) unvisitedNeighbours.Add(maze[x,y + 2]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneraorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current,chosen);
                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
            }    
            else
            {
                current = stack.Pop();
            }

        }while (stack.Count > 0);
    }
    private void RemoveWall(MazeGeneraorCell a, MazeGeneraorCell b)
    {
        if (a.X  == b.X)
        {
            if (a.Y > b.Y) a._bottomSide = false;
            else b._bottomSide = false;
        }
        else
        {
            if (a.X > b.X) a._leftSide = false;
            else b._leftSide = false;
        }
    }
}
