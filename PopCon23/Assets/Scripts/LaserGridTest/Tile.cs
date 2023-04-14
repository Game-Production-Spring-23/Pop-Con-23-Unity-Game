using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x_cord;
    public int y_cord;

    private int EntryPointNum;

    public GameObject[] paths;
    // Paths goes in clockwise pattern
    /* 0 = N 
     * 1 = NE 
     * 2 = SE 
     * 3 = S 
     * 4 = SW
     * 5 = NW
    */
    public Material Player1Color;
    public Material Player2Color;
    public Material Crossbeams;

    public bool MirrorStage1NS;
    public bool MirrorStage2NNESSW;
    public bool MirrorStage3NNWSSE;
    public bool MirrorStage4ENEWSW;
    public bool MirrorStage5WNWESE;
    public bool MirrorStage6EW;
    public bool MirrorStage7NorthSplitter;
    public bool MirrorStage8SouthSplitter;
    public bool MirrorStage9Blocker;


    public GameObject MirrorStage1NSObject;
    public GameObject MirrorStage2NNESSWObject;
    public GameObject MirrorStage3NNWSSEObject;
    public GameObject MirrorStage4ENEWSWObject;
    public GameObject MirrorStage5WNWESEObject;
    public GameObject MirrorStage6EWObject;
    public GameObject MirrorStage7NorthSplitterObject;
    public GameObject MirrorStage8SouthSplitterObject;
    public GameObject MirrorStage9BlockerObject;

    //Indications of whether the tile currently has any items or not
    public bool isEmpty = true;
    public bool hasMirror = false;
    public bool hasSplitter = false;
    public bool hasBlocker = false;

    public bool NorthStart;
    public bool SouthStart;

    void Start()
    {
    }
    public void StartLaser(Material player, string direction)
    {
        switch (direction){
            case "SEDirection":
                LaserTurnOn(2, player);
                // IF SW PATH IS LAST PATH, CALL HEX SW OF CURRENT HEX
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(2, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                }
            break;
            case "SWDirection":
                LaserTurnOn(4, player);
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(4, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                }
            break;
            case "NEDirection":
                LaserTurnOn(1, player);
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(1, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                }
            break;
            case "NWDirection":
                LaserTurnOn(5, player);
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(5, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                }
            break;
            case "NDirection":
                LaserTurnOn(0, player);
                // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(0, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                }
            break;
            case "SDirection":
                LaserTurnOn(3, player);
                // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
                if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6EW || MirrorStage7NorthSplitter || MirrorStage8SouthSplitter || MirrorStage9Blocker)
                {
                    if (LaserReflect(3, player) == false)
                    {
                        return;
                    }
                }
                else
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                }
            break;
            default:
            Debug.Log(direction);
            break;
        }
    }

    //Tells Tile Next door according to where the laser comes out to start their laser script
    public void ContinueLaser(int x, int y, Material player, string direction)
    {
        try 
        {
            GameManager.instance.gridArray[x_cord + x, y_cord + y].StartLaser(player, direction);
        }
        catch (System.IndexOutOfRangeException ex)
        {
            return;
        }
        catch (System.StackOverflowException ex){
            return;
        }
    }
    public bool LaserReflect(int entry, Material player)
    {
        switch (entry){
            case 0:
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1,-1,player, "NEDirection");
                    return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
                if (MirrorStage7NorthSplitter) {
                   LaserTurnOn(4, player);
                   LaserTurnOn(2, player);
                   ContinueLaser(1,-1,player, "NEDirection");
                   ContinueLaser(-1, -1, player, "NWDirection");
                   return true;
                }
            break;
            case 1:
                if (MirrorStage1NS)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage8SouthSplitter) {
                    LaserTurnOn(3, player);
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
            break;
            case 2:
                if (MirrorStage1NS)
                {
                   LaserTurnOn(1, player);
                   ContinueLaser(-1, 1, player, "SWDirection");
                   return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                    return true;
                }
                if (MirrorStage7NorthSplitter) {
                    LaserTurnOn(4, player);
                    LaserTurnOn(0, player);
                    ContinueLaser(1,-1,player, "NEDirection");
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
            break;
            case 3:
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                    return true;
                }
                if (MirrorStage8SouthSplitter) {
                    LaserTurnOn(1, player);
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
            break;
            case 4:
                if (MirrorStage1NS)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage2NNESSW)
                {
                    LaserTurnOn(0, player);
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
                if (MirrorStage5WNWESE)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    return true;
                }
                if (MirrorStage7NorthSplitter) {
                    LaserTurnOn(0, player);
                    LaserTurnOn(2, player);
                    ContinueLaser(-1, -1, player, "NWDirection");
                    ContinueLaser(0, 2, player, "SDirection");
                    return true;
                }
            break;
            case 5:
                if (MirrorStage1NS)
                {
                    LaserTurnOn(4, player);
                    ContinueLaser(1, -1, player, "NEDirection");
                    return true;
                }
                if (MirrorStage3NNWSSE)
                {
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    return true;
                }
                if (MirrorStage4ENEWSW)
                {
                    LaserTurnOn(5, player);
                    ContinueLaser(1, 1, player, "SEDirection");
                    return true;
                }
                if (MirrorStage6EW)
                {
                    LaserTurnOn(1, player);
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
                if (MirrorStage8SouthSplitter) {
                    LaserTurnOn(1, player);
                    LaserTurnOn(3, player);
                    ContinueLaser(0, -2, player, "NDirection");
                    ContinueLaser(-1, 1, player, "SWDirection");
                    return true;
                }
            break;
        }
        return false;
    }
    public void LaserTurnOn(int entryPoint, Material player)
    {
        if (paths[entryPoint].activeSelf == true) {
            paths[entryPoint].GetComponent<Renderer>().material = Crossbeams;
        }
        else {
            paths[entryPoint].SetActive(true);
            paths[entryPoint].GetComponent<Renderer>().material = player;
        }
    }

    void OnMouseOver()
    {

        Debug.Log("Mouse is over");
        if (!Input.GetMouseButton(0) && TurnBasedSystem.draggingItem == true && isEmpty == false)
        {
            //If statements to act according to which item the player has dropped onto the tile
            if(TurnBasedSystem.currentItem == 1)
            {
                hasMirror = true;
            }

            else if(TurnBasedSystem.currentItem == 2)
            {
                hasSplitter = true;
            }

            else if(TurnBasedSystem.currentItem == 3)
            {
                hasBlocker = true;
            }

        }
    }

}