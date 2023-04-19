using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{

    public static int playerTurn = 1;

    public static bool draggingItem = false;

    //used for the confirmation of placing an item
    public static bool isHovering = false;

    public static Tile curTile;

    //static variable to keep track of whether or not the player has confirmed their action
    public static bool confirmAction = false;

    //static bool to keep track of whether or not this script should instantiate new items at the given spots
    public static bool nextTurn = false;


    //Run this method if the player needs to put the item back in its spot
    public static bool resetItem = false;

    public static DragDrop draggedItem;

    //static int that keeps track of which turn the players are on
    public static int turnNumber = 1;

    //static bool that will prevent players from dragging new items onto the board if they have already made an action
    public static bool canDrag = true;

    public static bool beginNextTurn = false;

    //apublic arrays to instantiate the item prefabs
    public GameObject[] Items;
    public Vector3[] ItemSpawn;




    //I am creating this static int to keep track of which item is currently being dragged so that the system knows what to place on the tile
    public static int currentItem;

    void Start()
    {
        BeginTurn();
    }

    void Update()
    {
        if(confirmAction == true)
        {
            if(currentItem == 1)
            {
                curTile.hasMirror = true;
                curTile.SetTurn();
            }

            else if(currentItem == 2)
            {
                curTile.hasSplitter = true;
                curTile.SetTurn();
            }

            else if(currentItem == 3)
            {
                curTile.hasBlocker = true;
            }

            //in the case where a player uses a rock
            else if(currentItem == 4)
            {
                curTile.usedRock = true;
            }

            else if(currentItem == 5)
            {
                curTile.usedRotator = true;
            }

            else if(currentItem == 6)
            {
                curTile.personalMirrorOwner = turnNumber % 2;
                curTile.personalMirror = true;
                curTile.SetTurn();
            }

            else if(currentItem == 7)
            {
                curTile.usedBigRock = true;
                curTile.usedRock = true;
            }

            curTile.InitSpawn();

            confirmAction = false;

            isHovering = false;

            //set the tiles current turn to whatever this files current turn number is
            
        }

        if(resetItem == true)
        {
            draggedItem.ReplaceItem();
            resetItem = false;
        }

        if(beginNextTurn == true)
        {
            BeginTurn();
            beginNextTurn = false;
        }

    }

    public void BeginTurn()
    {
        //first we must get rid of any of the items that currently are on the board

        GameObject [] gameObjects = GameObject.FindGameObjectsWithTag("Item");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }

        //we will take in three random variables that will determine what item we should spawn into the world
        int ran1 = Random.Range(1, 100);
        int ran2 = Random.Range(1, 100);
        int ran3 = Random.Range(1, 100);

        int[] ranNums = { ran1, ran2, ran3 };

        SpawnInventory(ranNums);
    }


    //method to instantiate the players inventory
    void SpawnInventory(int [] ranNums)
    {
        //we will instantiate the items based on the random numbers generated
        for(int i = 0; i < 3; i++)
        {
          //Vector3 itemPosition = new Vector3(356, 158, 0);  
          if(ranNums[i] <= 23)
            {
                GameObject item1 = Instantiate(Items[0], ItemSpawn[i], Quaternion.Euler(new Vector3(0, 0, -90)));
                item1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                GameObject item2 = Instantiate(Items[0], ItemSpawn[i + 3], Quaternion.Euler(new Vector3(0, 0, -90)));
                item2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }
          
          else if(ranNums[i] > 23 && ranNums[i] <= 37)
            {
                GameObject item1 = Instantiate(Items[1], ItemSpawn[i], Quaternion.Euler(new Vector3(0, 0, -90)));
                item1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                GameObject item2 = Instantiate(Items[1], ItemSpawn[i + 3], Quaternion.Euler(new Vector3(0, 0, -90)));
                item2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }

          else if(ranNums[i] > 37 && ranNums[i] <= 46)
            {
                GameObject item1 = Instantiate(Items[2], ItemSpawn[i], Quaternion.Euler(new Vector3(0, 0, -90)));
                item1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                GameObject item2 = Instantiate(Items[2], ItemSpawn[i + 3], Quaternion.Euler(new Vector3(0, 0, -90)));
                item2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }

          else if(ranNums[i] > 46 && ranNums[i] <= 50)
            {
                GameObject item1 = Instantiate(Items[3], ItemSpawn[i], Quaternion.Euler(new Vector3(0, 0, -90)));
                item1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                GameObject item2 = Instantiate(Items[3], ItemSpawn[i + 3], Quaternion.Euler(new Vector3(0, 0, -90)));
                item2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }

          else if(ranNums[i] > 50 && ranNums[i] <= 54)
            {
                GameObject item1 = Instantiate(Items[6], ItemSpawn[i], Quaternion.Euler(new Vector3(0, 0, -90)));
                item1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                GameObject item2 = Instantiate(Items[6], ItemSpawn[i + 3], Quaternion.Euler(new Vector3(0, 0, -90)));
                item2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }

          else if(ranNums[i] > 54 && ranNums[i] <= 68)
            {
                GameObject item1 = Instantiate(Items[5], ItemSpawn[i], Quaternion.Euler(new Vector3(0, 0, -90)));
                item1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                GameObject item2 = Instantiate(Items[5], ItemSpawn[i + 3], Quaternion.Euler(new Vector3(0, 0, -90)));
                item2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }

          else if(ranNums[i] > 68 && ranNums[i] <= 78)
            {
                GameObject item1 = Instantiate(Items[4], ItemSpawn[i], Quaternion.Euler(new Vector3(0, 0, -90)));
                item1.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                GameObject item2 = Instantiate(Items[4], ItemSpawn[i + 3], Quaternion.Euler(new Vector3(0, 0, -90)));
                item2.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }
            else
            {
                //nothing
            }

         
            
          

        }
    }

    
}
