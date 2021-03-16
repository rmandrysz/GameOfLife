using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    public bool isAlive = false;
    public int numberOfNeighbors = 0;

    public void setAlive(bool alive)
    {
        isAlive = alive;

        if (alive)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
