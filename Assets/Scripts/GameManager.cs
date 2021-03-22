using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    private const int screenWidth = 2560 / 16;
    private const int screenHeight = 1440 / 16;

    public int horizontalCellCount = 10;
    public int verticalCellCount = 10;
    private int totalCellCount;

    private GameObject mainCamera;

    private Cell[,] grid;

    public float delay = 1f / 2f;
    private float nextUpdate = 0f;
    private bool isRunning = false;

    private int spawnChance = 0;

    private int writeLimit = 1000;
    private int writeCounter = 0;
    private int iterations = 0;
    public int maxIterations = 1;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Cell[horizontalCellCount, verticalCellCount];
        nextUpdate = delay;
        CreateBoard();
        Directory.CreateDirectory(Application.dataPath + "/Data/Errors/" + verticalCellCount + "/");
        totalCellCount = verticalCellCount * horizontalCellCount;
    }

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.transform.position = new Vector3(horizontalCellCount / 2, (verticalCellCount / 2) - 0.5f, -1);
        mainCamera.GetComponent<Camera>().orthographicSize = verticalCellCount / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Time.time >= nextUpdate)
        //{
            UpdateCells();
            //nextUpdate += delay;
        //}
    }

    public void GenerateCells()
    {

            for (int x = 0; x < horizontalCellCount; ++x)
            {
                for (int y = 0; y < verticalCellCount; ++y)
                {
                    grid[x, y].setAlive(Random.Range(0, 100) < spawnChance);

                }
            }
    }

    public void CreateBoard()
    {
        for (int x = 0; x < horizontalCellCount; ++x)
        {
            for (int y = 0; y < verticalCellCount; ++y)
            {
                GameObject cellPrefab = (GameObject)Resources.Load("Prefabs/Cell");
                grid[x, y] = Instantiate(cellPrefab, new Vector2(x, y), Quaternion.identity).GetComponent<Cell>();
            }
        }
    }

    private void UpdateCells()
    {
        float aliveCells = 0;

        if (isRunning)
        {
            CountCellNeighbors();

            for (int x = 0; x < horizontalCellCount; ++x)
            {
                for (int y = 0; y < verticalCellCount; ++y)
                {
                    if(grid[x, y].isAlive)
                    {
                        ++aliveCells;
                    }
                    if (grid[x, y].numberOfNeighbors == 3)
                    {
                        grid[x, y].setAlive(true);
                    }
                    else if (grid[x, y].numberOfNeighbors != 2)
                    {
                        grid[x, y].setAlive(false);
                    }
                }
            }
            //SaveToFile(aliveCells);
            SaveToFile2(aliveCells / totalCellCount);
        }
    }

    private void CountCellNeighbors()
    {
        for (int x = 0; x < horizontalCellCount; ++x)
        {
            for (int y = 0; y < verticalCellCount; ++y)
            {
                int neighbors = 0;

                //Debug.Log(x + "\t" + y + grid[x, y].GetComponent<Cell>().isAlive);

                // ---------------------------------------------------------------------------------------------------------------
                // North
                if (y + 1 < verticalCellCount)
                {
                    if (grid[x, y + 1].GetComponent<Cell>().isAlive) { 
                        ++neighbors;
                    }

                    // Northeast 
                    if (x + 1 < horizontalCellCount)
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
                        if (grid[horizontalCellCount - 1, y + 1].GetComponent<Cell>().isAlive)
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
                    if (x + 1 < horizontalCellCount)
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
                        if (grid[horizontalCellCount - 1, 0].GetComponent<Cell>().isAlive)
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
                    if (x + 1 < horizontalCellCount)
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
                        if (grid[horizontalCellCount - 1, y - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                }
                else
                {
                    if (grid[x, verticalCellCount - 1].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }
                                        
                    // Southeast 
                    if (x + 1 < horizontalCellCount)
                    {
                        if (grid[x + 1, verticalCellCount - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[0, verticalCellCount - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }

                    // Southwest
                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, verticalCellCount - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                    else
                    {
                        if (grid[horizontalCellCount - 1, verticalCellCount - 1].GetComponent<Cell>().isAlive)
                        {
                            ++neighbors;
                        }
                    }
                }
                // ---------------------------------------------------------------------------------------------------------------
                // West
                if (x + 1 < horizontalCellCount) {
                    if (grid[x + 1, y].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }
                }
                else
                {
                    if (grid[0, y].GetComponent<Cell>().isAlive)
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
                    if (grid[horizontalCellCount - 1, y].GetComponent<Cell>().isAlive)
                    {
                        ++neighbors;
                    }
                }

                grid[x, y].numberOfNeighbors = neighbors;
            }
        }
    }

    public void SimStart()
    {
        isRunning = true;
    }

    public void SimStop()
    {
        isRunning = false;
    }

    public void SimStep()
    {
        StartCoroutine(WaitOneFrame());
    }

    private IEnumerator WaitOneFrame()
    {
        SimStart();
        yield return new WaitForEndOfFrame();
        SimStop();
    }

    public bool SimRunning()
    {
        return isRunning;
    }

    public void OnSliderValueChange(float value)
    {
        spawnChance = (int)value;
    }

    public void SaveToFile(int value)
    {
        ++writeCounter;
        if(writeCounter > writeLimit)
        {
            Debug.Log(true);
            SimStop();
        }
        else 
        { 
            File.AppendAllText(Application.dataPath + "/Data/pEquals" + spawnChance + ".txt", value + "\n");
            Debug.Log(writeCounter);
        }
    }

    public void SaveToFile2(float value)
    {
        string path = Application.dataPath + "/Data/Errors/" + verticalCellCount + "/";
        if (writeCounter > writeLimit)
        {
            Debug.Log("Iteration" + iterations);
            ++iterations;
            if(iterations >= maxIterations)
            {
                SimStop();
                return;
            }
            else
            {
                GenerateCells();
            }
            writeCounter = 0;
        }
        File.AppendAllText(path + iterations + "pEq" + spawnChance + ".txt", (value + "\n").Replace(',', '.'));

        ++writeCounter;

    }
}
