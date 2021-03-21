using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public bool isAlive = false;
    public int numberOfNeighbors = 0;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    public void setAlive(bool alive)
    {
        isAlive = alive;

        if (alive)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        //Debug.Log("OnMouseDown()");
        if (!gameManager.SimRunning())
        {
            //Debug.Log("!gameManager.SimRunning");
            setAlive(!isAlive);
        }
    }
}
