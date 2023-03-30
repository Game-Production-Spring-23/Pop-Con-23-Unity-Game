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
    public bool MirrorStage6NorthSplitter;
    public bool MirrorStage7SouthSplitter;
    public bool MirrorStage8Blocker;


    public GameObject MirrorStage1NSObject;
    public GameObject MirrorStage2NNESSWObject;
    public GameObject MirrorStage3NNWSSEObject;
    public GameObject MirrorStage4ENEWSWObject;
    public GameObject MirrorStage5WNWESEObject;
    public GameObject MirrorStage6NorthSplitterObject;
    public GameObject MirrorStage7SouthSplitterObject;
    public GameObject MirrorStage8BlockerObject;

    public bool NorthStart;
    public bool SouthStart;

    void Start()
    {
    }
    public void StartLaser(Material player, string direction)
    {
        if (direction == "SEDirection")
        {
            LaserTurnOn(2, player);
            // IF SW PATH IS LAST PATH, CALL HEX SW OF CURRENT HEX
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
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
        }
        else if (direction == "SWDirection")
        {
            LaserTurnOn(4, player);
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
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
        }
        else if (direction == "NEDirection")
        {
            LaserTurnOn(1, player);
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE|| MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
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
        }
        else if (direction == "NWDirection")
        {
            LaserTurnOn(5, player);
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE|| MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
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
        }
        else if (direction == "NDirection")
        {
            LaserTurnOn(0, player);
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE|| MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
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
        }
        else if (direction == "SDirection")
        {
            LaserTurnOn(3, player);
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE|| MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
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
        }
        else
        {
            Debug.Log(direction);
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
        if (entry == 0)
        {
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
            if (MirrorStage6NorthSplitter) {
                LaserTurnOn(4, player);
                LaserTurnOn(2, player);
                ContinueLaser(1,-1,player, "NEDirection");
                ContinueLaser(-1, -1, player, "NWDirection");
                return true;
            }
        }
        else if (entry == 1)
        {
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
            if (MirrorStage4ENEWSW)
            {
                LaserTurnOn(5, player);
                ContinueLaser(1, 1, player, "SEDirection");
                return true;
            }
            if (MirrorStage5WNWESE)
            {
                LaserTurnOn(0, player);
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
            if (MirrorStage7SouthSplitter) {
                LaserTurnOn(3, player);
                LaserTurnOn(5, player);
                ContinueLaser(1, 1, player, "SEDirection");
                ContinueLaser(0, -2, player, "NDirection");
                return true;
            }
        }
        else if (entry == 2)
        {
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
            if (MirrorStage5WNWESE)
            {
                LaserTurnOn(4, player);
                ContinueLaser(1, -1, player, "NEDirection");
                return true;
            }
            if (MirrorStage6NorthSplitter) {
                LaserTurnOn(4, player);
                LaserTurnOn(0, player);
                ContinueLaser(1,-1,player, "NEDirection");
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
        }
        else if (entry == 3)
        {
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
            if (MirrorStage7SouthSplitter) {
                LaserTurnOn(1, player);
                LaserTurnOn(5, player);
                ContinueLaser(1, 1, player, "SEDirection");
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
        }
        else if (entry == 4)
        {
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
            if (MirrorStage4ENEWSW)
            {
                LaserTurnOn(2, player);
                ContinueLaser(-1, -1, player, "NWDirection");
                return true;
            }
            if (MirrorStage5WNWESE)
            {
                LaserTurnOn(3, player);
                ContinueLaser(0, -2, player, "NDirection");
                return true;
            }
            if (MirrorStage6NorthSplitter) {
                LaserTurnOn(0, player);
                LaserTurnOn(2, player);
                ContinueLaser(-1, -1, player, "NWDirection");
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
        }
        else if (entry == 5)
        {
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
            if (MirrorStage5WNWESE)
            {
                LaserTurnOn(1, player);
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
            if (MirrorStage7SouthSplitter) {
                LaserTurnOn(1, player);
                LaserTurnOn(3, player);
                ContinueLaser(0, -2, player, "NDirection");
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
        }
        else
        {
            return false;
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



}