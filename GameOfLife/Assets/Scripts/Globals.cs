using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    DEAD = 0,
    DEADNEXT = 1,
    ENEMY = 2,
    ENEMYNEXT = 3,
    PLAYER = 4,
    PLAYERNEXT = 5,
    WALL = 6
}

public enum SeedType
{
    DIAMOND = 0,
    GLIDER_R = 1,
    GLIDER_L = 2,
    SPACESHIP = 3
}

public class Globals : MonoBehaviour {
    public static InitialCube[,] cubeGrid;
    public static InitialCube[,] tempCubeGrid;
    public static readonly int level1turns = 25;
    public static readonly int level2turns = 30;
    public static readonly int level3turns = 35;
    public static readonly int level4turns = 40;
    public static readonly int level5turns = 45;
    public Transform cubePrefab;
    public static SeedFactory factory = new SeedFactory();

    public static bool completeTurnRequested = false;
    public static bool paintAllowed = false;
    public static bool panningAllowed = false;
    public static bool pasting = false;

    public static SeedType whichSeed;
    public static bool initPasteGrid = false;

    public static int end_total_player;
    public static int end_total_enemy;

    // Next update in second
    private int nextUpdate = 1;

    // Use this for initialization
    void Start () {
        initGrid();
    }

    // Update is called once per frame
    void Update () {
        if(completeTurnRequested)
        {
            step(PlayerType.PLAYER);
            step(PlayerType.ENEMY);
            completeTurnRequested = false;
        }
    }

    public void initGrid()
    {
        Debug.Log("Resetting Grid");
        cubeGrid = new InitialCube[24, 24];
        for (var i = 0; i < 24; i++)
        {
            for (var j = 0; j < 24; j++)
            {
                cubeGrid[i, j] = new InitialCube(cubePrefab);
                cubeGrid[i, j].prefab = Instantiate(cubePrefab, new Vector3(i * 1.5f, 0.5f, j * 1.5f), Quaternion.identity);
                if ((j == 0 || j == 23) || (i == 0 || i == 23))
                {
                    cubeGrid[i, j].SetPlayerType(PlayerType.WALL);
                }
                else
                    cubeGrid[i, j].SetPlayerType(PlayerType.DEAD);
            }
        }

        cubeGrid[5, 20].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[4, 19].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[6, 19].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[5, 18].SetPlayerType(PlayerType.ENEMY);

        cubeGrid[12, 20].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[11, 19].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[13, 19].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[12, 18].SetPlayerType(PlayerType.ENEMY);

        cubeGrid[19, 20].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[18, 19].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[20, 19].SetPlayerType(PlayerType.ENEMY);
        cubeGrid[19, 18].SetPlayerType(PlayerType.ENEMY);
    }

    public void step(PlayerType currentPlayer)
    {
        InitialCube[,] future = new InitialCube[24, 24];

        foreach (var cell in cubeGrid)
        {
            Destroy(cell.prefab.gameObject);
        }

        for (var i = 0; i < 24; i++)
        {
            for (var j = 0; j < 24; j++)
            {
                future[i, j] = new InitialCube(cubePrefab);
                future[i, j].prefab = Instantiate(cubePrefab, new Vector3(i * 1.5f, 0.5f, j * 1.5f), Quaternion.identity);
                if((j == 0 || j == 23) || (i == 0 || i == 23))
                {
                    future[i, j].SetPlayerType(PlayerType.WALL);
                }
                else 
                    future[i, j].SetPlayerType(PlayerType.DEAD);
            }
        }

        // Loop through every cell
        for (int l = 1; l < 24 - 1; l++)
        {
            for (int m = 1; m < 24 - 1; m++)
            {
                // finding no Of Neighbours that are alive
                int aliveNeighbours = 0;
                for (int i = -1; i <= 1; i++)
                     for (int j = -1; j <= 1; j++)
                        aliveNeighbours += cubeGrid[l + i, m + j].playerType == currentPlayer ? 1 : 0;

                // The cell needs to be subtracted from
                // its neighbours as it was counted before
                aliveNeighbours -= cubeGrid[l, m].playerType == currentPlayer ? 1: 0;

                // Implementing the Rules of Life

                // Cell is lonely and dies
                if ((cubeGrid[l, m].playerType == currentPlayer) && (aliveNeighbours < 2))
                    future[l, m].SetPlayerType(PlayerType.DEAD);

                // Cell dies due to over population
                else if ((cubeGrid[l, m].playerType == currentPlayer) && (aliveNeighbours > 3))
                    future[l, m].SetPlayerType(PlayerType.DEAD);

                // A new cell is born
                else if ((cubeGrid[l, m].playerType == PlayerType.DEAD) && (aliveNeighbours == 3))
                    future[l, m].SetPlayerType(currentPlayer);

                // Remains the same
                else
                    future[l, m].SetPlayerType(cubeGrid[l, m].playerType);
            }
        }

        for (int l = 1; l < 24 - 1; l++)
        {
            for (int m = 1; m < 24 - 1; m++)
            {
                if(cubeGrid[l, m].playerType == PlayerType.PLAYER && cubeGrid[l+1, m].playerType == PlayerType.ENEMY ||
                    cubeGrid[l, m].playerType == PlayerType.ENEMY && cubeGrid[l+1, m].playerType == PlayerType.PLAYER)
                {
                    future[l, m].SetPlayerType(PlayerType.DEAD);
                    future[l + 1, m].SetPlayerType(PlayerType.DEAD);
                }
                if (cubeGrid[l, m].playerType == PlayerType.PLAYER && cubeGrid[l - 1, m].playerType == PlayerType.ENEMY ||
                    cubeGrid[l, m].playerType == PlayerType.ENEMY && cubeGrid[l - 1, m].playerType == PlayerType.PLAYER)
                {
                    future[l, m].SetPlayerType(PlayerType.DEAD);
                    future[l - 1, m].SetPlayerType(PlayerType.DEAD);
                }
                if (cubeGrid[l, m].playerType == PlayerType.PLAYER && cubeGrid[l, m + 1].playerType == PlayerType.ENEMY ||
                    cubeGrid[l, m].playerType == PlayerType.ENEMY && cubeGrid[l, m + 1].playerType == PlayerType.PLAYER)
                {
                    future[l, m].SetPlayerType(PlayerType.DEAD);
                    future[l, m + 1].SetPlayerType(PlayerType.DEAD);
                }
                if (cubeGrid[l, m].playerType == PlayerType.PLAYER && cubeGrid[l, m - 1].playerType == PlayerType.ENEMY ||
                    cubeGrid[l, m].playerType == PlayerType.ENEMY && cubeGrid[l, m - 1].playerType == PlayerType.PLAYER)
                {
                    future[l, m].SetPlayerType(PlayerType.DEAD);
                    future[l, m - 1].SetPlayerType(PlayerType.DEAD);
                }
            }
        }
        cubeGrid = future;

    }
}
