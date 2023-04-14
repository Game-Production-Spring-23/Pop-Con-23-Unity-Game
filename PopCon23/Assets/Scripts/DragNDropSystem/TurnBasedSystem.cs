using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{

    public static int playerTurn = 1;

    public static bool draggingItem = false;


    //I am creating this static int to keep track of which item is currently being dragged so that the system knows what to place on the tile
    public static int currentItem;

    void Start()
    {
        Debug.Log("Let the games begin");
    }

    void Update()
    {
        
    }

}
