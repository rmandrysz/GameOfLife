using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int screenWidth = 2560 / 16;
    private const int screenHeight = 1440 / 16;
    private Cell[,] grid = new Cell[screenWidth, screenHeight];

    public float delay = 1f / 2f;
    private float nextUpdate = 0f;

    public int spawnChance = 30;

    // Start is called before the first frame update
    void Start()
    {
        nextUpdate = delay;
        generateCells(spawnChance);
    }

    void Awake()
    {
        Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Time.time >= nextUpdate)
        //{
            updateCells();
            //nextUpdate += delay;
        //}
    }

    private void generateCells()
    {
        for (int x = 0; x < screenWidth; ++x)
        {
            for (int y = 0; y < screenHeight; ++y)
            {
                GameObject cellPrefab = (GameObject)Resources.Load("Prefabs/Cell");
                grid[x, y] = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<Cell>();
                grid[x, y].setAlive(true);
            }
        }
    }

    private void generateCells(int aliveChance)
    {
        for (int x = 0; x < screenWidth; ++x)
        {
            for (int y = 0; y < screenHeight; ++y)
            {
                GameObject cellPrefab = (GameObject)Resources.Load("Prefabs/Cell");
                grid[x, y] = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<Cell>();
                
                grid[x, y].setAlive(Random.Range(0, 100) < aliveChance);
            }
        }
    }

    private void updateCells()
    {
        countCellNeighbors();

        for (int x = 0; x < screenWidth; ++x)
        {
            for (int y = 0; y < screenHeight; ++y)
            {
                if (grid[x, y].numberOfNeighbors > 3 || grid[x, y].numberOfNeighbors < 2)
                {
                    grid[x, y].setAlive(false);
                }
                else
                {
                    grid[x, y].setAlive(true);
                }
            }
        }
    }

    private void countCellNeighbors()
    {
        for (int x = 0; x < screenWidth; ++x)
        {
            for (int y = 0; y < screenHeight; ++y)
            {
                int neighbors = 0;

                //Debug.Log(x + "\t" + y + grid[x, y].GetComponent<Cell>().isAlive);

                // ---------------------------------------------------------------------------------------------------------------
                // North
                if (y + 1 < screenHeight)
                {
                    if (grid[x, y + 1].GetComponent<Cell>().isAlive) { 
                        ++neighbors;
                    }

                    // Northeast 
                    if (x + 1 < screenWidth)
                    {
                        if (grid[x + 1, y + 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[0, y + 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }

                    // Northwest
                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, y + 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    } 
                    else
                    {
                        if (grid[screenWidth - 1, y + 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                }
                else
                {
                    if (grid[x, 0].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }

                    // Northeast
                    if (x + 1 < screenWidth)
                    {
                        if (grid[x + 1, 0].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[0, 0].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }

                    // Northwest
                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, 0].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[screenWidth - 1, 0].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                }
                // ---------------------------------------------------------------------------------------------------------------
                // South
                if (y - 1 >= 0)
                {
                    if (grid[x, y - 1].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }

                    // Northeast 
                    if (x + 1 < screenWidth)
                    {
                        if (grid[x + 1, y - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[0, y - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }

                    // Northwest
                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, y - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[screenWidth - 1, y - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                }
                else
                {
                    if (grid[x, screenHeight - 1].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }
                                        
                    // Southeast 
                    if (x + 1 < screenWidth)
                    {
                        if (grid[x + 1, screenHeight - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[0, screenHeight - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }

                    // Southwest
                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, screenHeight - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[screenWidth - 1, screenHeight - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                }
                // ---------------------------------------------------------------------------------------------------------------
                // West
                if (x + 1 < screenWidth) {
                    if (grid[x + 1, y].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }
                }
                else
                {
                    if (grid[0, y])
                    {
                        ++neighbors;
                    }

                }
                // ---------------------------------------------------------------------------------------------------------------
                // East
                if (x - 1 >= 0)
                {
                    if (grid[x - 1, y].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }
                }
                else
                {
                    if (grid[screenWidth - 1, y].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }
                }

                grid[x, y].numberOfNeighbors = neighbors;
            }
        }
    }
}
