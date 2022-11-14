
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeCraete : MonoBehaviour
{
    [SerializeField] private GameObject[] _roadOneWayOut;
    [SerializeField] private GameObject[] _roadTwoWayOut;
    [SerializeField] private GameObject[] _roadTwoWayOutTurn;
    [SerializeField] private GameObject[] _roadThreeWayOut;
    [SerializeField] private GameObject[] _roadFourWayOut;
    [SerializeField] private GameObject[] _blockCell;
    [SerializeField] private GameObject[] _blockBetweenMazeTwoWayOut;
    [SerializeField] private GameObject[] _blockBetweenMazeOneWayOut;
    [SerializeField] private GameObject[] _blockBetweenMazeFourWayOut;
    [SerializeField] private GameObject[] _blockBetweenMazeClear;
    [SerializeField] private GameObject _zone;

    private List<Vector3> _spawnPoint;

    [SerializeField] private NavMeshSurface _surface;

    bool[] _border1;
    bool[] _border2;

    private bool[,] _remeberMatrixPosition = new bool[100,100];
    private GameObject[,] _zoneMatrix = new GameObject[100, 100];
    private  Zone[,] _zoneInfo = new Zone [100, 100];
    private int _matrixCenterCalibration;
    Stack<string> _midleCell =  new Stack<string>();

    private void Start()
    {
        _matrixCenterCalibration = (_remeberMatrixPosition.GetLength(0))/2;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                SpawnMaze(i + _matrixCenterCalibration, j + _matrixCenterCalibration);
            }
        }
        _surface.BuildNavMesh();
    }
    public void ChoseSideToSpawn(int changeNumX, int changeNumY)
    {

        _remeberMatrixPosition[changeNumX, changeNumY] = true;
        if(_remeberMatrixPosition[changeNumX + 1 , changeNumY] == true)
        {
            _remeberMatrixPosition[changeNumX + 1, changeNumY] = false;
            SpawnMaze(changeNumX - 1, changeNumY - 1);
            SpawnMaze(changeNumX - 1, changeNumY + 1);
            SpawnMaze(changeNumX - 1, changeNumY);

            _zoneInfo[changeNumX + 2, changeNumY - 1].DeleteZone();
            _zoneInfo[changeNumX + 2, changeNumY + 1].DeleteZone();
            _zoneInfo[changeNumX + 2, changeNumY].DeleteZone();

        }
        if (_remeberMatrixPosition[changeNumX - 1, changeNumY] == true)
        {
            _remeberMatrixPosition[changeNumX - 1, changeNumY] = false;
            SpawnMaze(changeNumX + 1, changeNumY - 1);
            SpawnMaze(changeNumX + 1, changeNumY + 1);
            SpawnMaze(changeNumX + 1, changeNumY);

            _zoneInfo[changeNumX - 2, changeNumY - 1].DeleteZone();
            _zoneInfo[changeNumX - 2, changeNumY + 1].DeleteZone();
            _zoneInfo[changeNumX - 2, changeNumY].DeleteZone();

        }
        if (_remeberMatrixPosition[changeNumX, changeNumY + 1] == true)
        {
            _remeberMatrixPosition[changeNumX, changeNumY + 1] = false;
            SpawnMaze(changeNumX - 1, changeNumY - 1);
            SpawnMaze(changeNumX + 1, changeNumY - 1);
            SpawnMaze(changeNumX, changeNumY - 1);

            _zoneInfo[changeNumX - 1, changeNumY + 2].DeleteZone();
            _zoneInfo[changeNumX + 1, changeNumY + 2].DeleteZone();
            _zoneInfo[changeNumX, changeNumY + 2].DeleteZone();

        }
        if (_remeberMatrixPosition[changeNumX, changeNumY - 1] == true)
        {
            _remeberMatrixPosition[changeNumX, changeNumY - 1] = false;
            SpawnMaze(changeNumX - 1, changeNumY + 1);
            SpawnMaze(changeNumX + 1, changeNumY + 1);
            SpawnMaze(changeNumX, changeNumY + 1);

            _zoneInfo[changeNumX - 1, changeNumY - 2].DeleteZone();
            _zoneInfo[changeNumX + 1, changeNumY - 2].DeleteZone();
            _zoneInfo[changeNumX, changeNumY - 2].DeleteZone();
        }
        _surface.BuildNavMesh();

    }
    public void SpawnMaze(int numX,int numY)
    {
        MazeGenerator _generator  = new MazeGenerator();
        MazeGeneraorCell[,] _mazes = _generator.GenerateNewMaze();
        float increaseX = numX * (_mazes.GetLength(0) - 1);
        float increaseY = numY * (_mazes.GetLength(1) - 1);
        BorderConstruction(_mazes,increaseX, increaseY);

        WorkWithZone(increaseX,increaseY,numX,numY);

        for (int i = 1; i < _mazes.GetLength(0); i += 2)
        {
            for (int j = 1; j < _mazes.GetLength(1); j += 2)
            {
                if (_mazes[i, j]._bottomSide == false)
                {
                    Instantiate(_roadTwoWayOut[Random.Range(0, _roadTwoWayOut.Length)], new Vector3((i + increaseX)  * 3.2f, 0, (j + increaseY) * 3.2f - 3.2f), Quaternion.identity);
                    _midleCell.Push("down");

                }
                else
                {
                    if (j > 2 )
                    {
                        Instantiate(_blockCell[Random.Range(0, _blockCell.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f - 3.2f), Quaternion.identity);
                    }
                }

                if (_mazes[i, j]._leftSide == false)
                {

                    Instantiate(_roadTwoWayOut[Random.Range(0, _roadTwoWayOut.Length)], new Vector3((i + increaseX) * 3.2f - 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, -1f, 0f, 1f));
                    _midleCell.Push("left");
                }
                else
                {
                    if (i > 2)
                    {
                        Instantiate(_blockCell[Random.Range(0, _blockCell.Length)], new Vector3((i + increaseX) * 3.2f - 3.2f, 0, (j + increaseY) * 3.2f), Quaternion.identity);
                    }
                }

                if (j < _generator.Width - 2 && _mazes[i, j + 2]._bottomSide == false)
                {
                    _midleCell.Push("up");
                }
                if (i < _generator.Height - 2 &&_mazes[i + 2, j]._leftSide == false)
                {
                    _midleCell.Push("right");
                }              
                ChoseMidleCell(i, j,increaseX,increaseY);
                _midleCell.Clear();
            }
        }
        SpawnBlockCell(_mazes,increaseX, increaseY);

    }

    private void WorkWithZone(float increaseX, float increaseY, int numX, int numY)
    {

        _zoneMatrix[numX,numY]  = Instantiate(_zone, new Vector3((6.4f + increaseX) * 3.2f, 0, (6.4f + increaseY) * 3.2f), Quaternion.identity) as GameObject;
        _zoneInfo[numX,numY] = _zoneMatrix[numX, numY].GetComponent<Zone>();
        _zoneInfo[numX, numY]._numArrayX = numX;
        _zoneInfo[numX, numY]._numArrayY = numY;
    }
    private void SpawnBlockCell(MazeGeneraorCell[,] _mazes, float increaseX, float increaseY)
    {
        for (int i = 2; i < _mazes.GetLength(0) - 2; i += 2)
        {
            for (int j = 2; j < _mazes.GetLength(1) - 2; j += 2)
            {
                Instantiate(_blockCell[Random.Range(0, _blockCell.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), Quaternion.identity);
            }
        }
    }

    private void ChoseMidleCell(int i, int j, float increaseX, float increaseY)
    {
        switch (_midleCell.Count)
        {
            case 1:
                if (_midleCell.Contains("up"))
                {
                    Instantiate(_roadOneWayOut[Random.Range(0, _roadOneWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 0f, 0f, 1f));
                }
                if (_midleCell.Contains("left"))
                {
                    Instantiate(_roadOneWayOut[Random.Range(0, _roadOneWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, -1f, 0f, 1f));
                }
                if (_midleCell.Contains("down"))
                {
                    Instantiate(_roadOneWayOut[Random.Range(0, _roadOneWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 10000f, 0f, 1f));
                }
                if (_midleCell.Contains("right"))
                {
                    Instantiate(_roadOneWayOut[Random.Range(0, _roadOneWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 1f, 0f, 1f));
                }
                break;
            case 2:
                if (_midleCell.Contains("up") && _midleCell.Contains("down"))
                {
                    Instantiate(_roadTwoWayOut[Random.Range(0, _roadTwoWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 0f, 0f, 1f));
                }
                if (_midleCell.Contains("left") && _midleCell.Contains("right"))
                {
                    Instantiate(_roadTwoWayOut[Random.Range(0, _roadTwoWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, -1f, 0f, 1f));
                }
                if (_midleCell.Contains("left") && _midleCell.Contains("up"))
                {
                    Instantiate(_roadTwoWayOutTurn[Random.Range(0, _roadTwoWayOutTurn.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 0f, 0f, 1f));
                }
                if (_midleCell.Contains("up") && _midleCell.Contains("right"))
                {
                    Instantiate(_roadTwoWayOutTurn[Random.Range(0, _roadTwoWayOutTurn.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 1f, 0f, 1f));
                }
                if (_midleCell.Contains("right") && _midleCell.Contains("down"))
                {
                    Instantiate(_roadTwoWayOutTurn[Random.Range(0, _roadTwoWayOutTurn.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 10000f, 0f, 1f));
                }
                if (_midleCell.Contains("down") && _midleCell.Contains("left"))
                {
                    Instantiate(_roadTwoWayOutTurn[Random.Range(0, _roadTwoWayOutTurn.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY)  * 3.2f), new Quaternion(0f, -1f, 0f, 1f));
                }
                break;
            case 3:
                if (_midleCell.Contains("down") && _midleCell.Contains("left") && _midleCell.Contains("up"))
                {
                    Instantiate(_roadThreeWayOut[Random.Range(0, _roadThreeWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 0f, 0f, 1f));
                }
                if (_midleCell.Contains("left") && _midleCell.Contains("up") && _midleCell.Contains("right"))
                {
                    Instantiate(_roadThreeWayOut[Random.Range(0, _roadThreeWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 1f, 0f, 1f));
                }
                if (_midleCell.Contains("up") && _midleCell.Contains("right") && _midleCell.Contains("down"))
                {
                    Instantiate(_roadThreeWayOut[Random.Range(0, _roadThreeWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, 10000f, 0f, 1f));
                }
                if (_midleCell.Contains("right") && _midleCell.Contains("down") && _midleCell.Contains("left"))
                {
                    Instantiate(_roadThreeWayOut[Random.Range(0, _roadThreeWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), new Quaternion(0f, -1f, 0f, 1f));
                }
                break;
            case 4:
                Instantiate(_roadFourWayOut[Random.Range(0, _roadFourWayOut.Length)], new Vector3((i + increaseX) * 3.2f, 0, (j + increaseY) * 3.2f), Quaternion.identity);
                break;
        }
    }
    private void BorderConstruction(MazeGeneraorCell[,] _mazes, float increaseX, float increaseY)
    {
        int _exitsNum1 = Random.Range(1, 3);
        int _exitsNum2 = Random.Range(1, 3);
        _border1 = new bool[_mazes.GetLength(0)];
        _border2 = new bool[_mazes.GetLength(1)];
        Instantiate(_blockBetweenMazeFourWayOut[Random.Range(0, _blockBetweenMazeFourWayOut.Length)], new Vector3((_mazes.GetLength(0) + increaseX - 1) * 3.2f, 0, (_mazes.GetLength(0) + increaseY - 1) * 3.2f), Quaternion.identity);
        if (_exitsNum1 > 1)
        {
            PlaceBorderExit(increaseX + 3, _mazes.GetLength(0) + increaseY - 1, true);
            _border1[3 - 1] = true;
            _border1[3 + 1] = true;
            _border1[3] = true;

            PlaceBorderExit((increaseX + _mazes.GetLength(1) - 4), _mazes.GetLength(0) + increaseY - 1, true);
            _border1[_mazes.GetLength(1) - 3] = true;
            _border1[_mazes.GetLength(1) - 4] = true;
            _border1[_mazes.GetLength(1) - 5] = true;
            PlaceTwoWayWalls(increaseX , _mazes.GetLength(0) + increaseY - 1, true, _mazes.GetLength(0));
        }
        else
        {
            PlaceBorderExit((increaseX - 1 + _mazes.GetLength(1) / 2), _mazes.GetLength(0) + increaseY - 1, true);
            _border1[_mazes.GetLength(1) / 2] = true;
            _border1[(_mazes.GetLength(1) / 2) - 1] = true;
            _border1[(_mazes.GetLength(1) / 2) - 2] = true;
            PlaceTwoWayWalls(increaseX , _mazes.GetLength(0) + increaseY - 1, true, _mazes.GetLength(0));
        }


        if (_exitsNum2 > 1)
        {
            PlaceBorderExit(_mazes.GetLength(1) + increaseX - 1, increaseY + 3, false);
            _border2[3 - 1] = true;
            _border2[3 + 1] = true;
            _border2[3] = true;

            PlaceBorderExit(_mazes.GetLength(1) + increaseX - 1, increaseY + _mazes.GetLength(0) - 4, false);
            _border2[_mazes.GetLength(0) - 3] = true;
            _border2[_mazes.GetLength(0) - 4] = true;
            _border2[_mazes.GetLength(0) - 5] = true;
            PlaceTwoWayWalls(_mazes.GetLength(1) + increaseX - 1, increaseY , false, _mazes.GetLength(1));
        }
        else
        {
            PlaceBorderExit(_mazes.GetLength(1) + increaseX - 1, increaseY - 1 + _mazes.GetLength(0) / 2, false);
            _border2[_mazes.GetLength(0) / 2 - 1] = true;
            _border2[(_mazes.GetLength(0) / 2) - 2] = true;
            _border2[(_mazes.GetLength(0) / 2) ] = true;
            PlaceTwoWayWalls(_mazes.GetLength(1) + increaseX - 1, increaseY , false, _mazes.GetLength(1));
        }
    }

    private void PlaceTwoWayWalls(float i, float j, bool left,int mazeLegth)
    {
        if (left == true)
        {
            for (int x = 1; x < mazeLegth - 1; x++)
            {
                if (_border1[x] != true)
                {
                    Instantiate(_blockBetweenMazeTwoWayOut[Random.Range(0, _blockBetweenMazeTwoWayOut.Length)], new Vector3((i+x) * 3.2f, 0, (j) * 3.2f), Quaternion.identity);
                    _border1[x] = true;
                }
            }
        }
        else
        {
            for (int x = 1; x < mazeLegth - 1; x++)
            {
                if (_border2[x] != true)
                {
                    Instantiate(_blockBetweenMazeTwoWayOut[Random.Range(0, _blockBetweenMazeTwoWayOut.Length)], new Vector3((i) * 3.2f, 0, (j+x) * 3.2f), new Quaternion(0f, 1f, 0f, 1f));
                    _border2[x] = true;
                }
            }
        }
    }
    private void PlaceBorderExit(float i ,float j,bool left)
    {
        if (left == true)
        {
            Instantiate(_blockBetweenMazeClear[Random.Range(0, _blockBetweenMazeClear.Length)], new Vector3((i) * 3.2f, 0, (j) * 3.2f), Quaternion.identity);

            Instantiate(_blockBetweenMazeOneWayOut[Random.Range(0, _blockBetweenMazeOneWayOut.Length)], new Vector3((i + 1) * 3.2f, 0, (j) * 3.2f), new Quaternion(0f, 10000f, 0f, 1f));
            Instantiate(_blockBetweenMazeOneWayOut[Random.Range(0, _blockBetweenMazeOneWayOut.Length)], new Vector3((i - 1) * 3.2f, 0, (j) * 3.2f), new Quaternion(0f, 0f, 0f, 1f));
        }
        else
        {
            Instantiate(_blockBetweenMazeClear[Random.Range(0, _blockBetweenMazeClear.Length)], new Vector3((i) * 3.2f, 0, (j) * 3.2f), Quaternion.identity);

            Instantiate(_blockBetweenMazeOneWayOut[Random.Range(0, _blockBetweenMazeOneWayOut.Length)], new Vector3((i ) * 3.2f, 0, (j + 1) * 3.2f), new Quaternion(0f, 1f, 0f, 1f));
            Instantiate(_blockBetweenMazeOneWayOut[Random.Range(0, _blockBetweenMazeOneWayOut.Length)], new Vector3((i ) * 3.2f, 0, (j - 1) * 3.2f), new Quaternion(0f, -1f, 0f, 1f));
        }
    }
}
