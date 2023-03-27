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
            Debug.Log("TEST");
            paths[2].SetActive(true);
            paths[2].GetComponent<Renderer>().material = player;
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
                paths[5].GetComponent<Renderer>().material = player;
                paths[5].SetActive(true);
                ContinueLaser(1, 1, player, "SEDirection");
            }
        }
        else if (direction == "SWDirection")
        {
            Debug.Log("TEST");
            paths[4].SetActive(true);
            paths[4].GetComponent<Renderer>().material = player;
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE || MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
            {
                if (LaserReflect(4, player) == false)
                {
                    return;
                }
            }
            else
            {
                paths[1].SetActive(true);
                paths[1].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, 1, player, "SWDirection");
            }
        }
        else if (direction == "NEDirection")
        {
            Debug.Log("TEST");
            paths[1].SetActive(true);
            paths[1].GetComponent<Renderer>().material = player;
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE|| MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
            {
                if (LaserReflect(1, player) == false)
                {
                    return;
                }
            }
            else
            {
                paths[4].SetActive(true);
                paths[4].GetComponent<Renderer>().material = player;
                ContinueLaser(1, -1, player, "NEDirection");
            }
        }
        else if (direction == "NWDirection")
        {
            Debug.Log("TEST");
            paths[5].SetActive(true);
            paths[5].GetComponent<Renderer>().material = player;
            if (MirrorStage1NS || MirrorStage2NNESSW || MirrorStage3NNWSSE || MirrorStage4ENEWSW || MirrorStage5WNWESE|| MirrorStage6NorthSplitter || MirrorStage7SouthSplitter || MirrorStage8Blocker)
            {
                if (LaserReflect(5, player) == false)
                {
                    return;
                }
            }
            else
            {
                paths[2].SetActive(true);
                paths[2].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, -1, player, "NWDirection");
            }
        }
        else if (direction == "NDirection")
        {
            Debug.Log("TEST");
            paths[0].SetActive(true);
            paths[0].GetComponent<Renderer>().material = player;
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
                paths[3].SetActive(true);
                paths[3].GetComponent<Renderer>().material = player;
                ContinueLaser(0, -2, player, "NDirection");
            }
        }
        else if (direction == "SDirection")
        {
            Debug.Log("TEST");
            paths[3].SetActive(true);
            paths[3].GetComponent<Renderer>().material = player;
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
                paths[0].SetActive(true);
                paths[0].GetComponent<Renderer>().material = player;
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
                paths[4].SetActive(true);
                paths[4].GetComponent<Renderer>().material = player;
                ContinueLaser(1,-1,player, "NEDirection");
                return true;
            }
            if (MirrorStage3NNWSSE)
            {
                paths[2].SetActive(true);
                paths[2].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, -1, player, "NWDirection");
                return true;
            }
            if (MirrorStage4ENEWSW)
            {
                paths[5].SetActive(true);
                paths[5].GetComponent<Renderer>().material = player;
                ContinueLaser(1, 1, player, "SEDirection");
                return true;
            }
            if (MirrorStage5WNWESE)
            {
                paths[1].SetActive(true);
                paths[1].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
            if (MirrorStage6NorthSplitter) {
                paths[2].SetActive(true);
                paths[4].SetActive(true);
                paths[2].GetComponent<Renderer>().material = player;
                paths[4].GetComponent<Renderer>().material = player;
                ContinueLaser(1,-1,player, "NEDirection");
                ContinueLaser(-1, -1, player, "NWDirection");
                return true;
            }
        }
        else if (entry == 1)
        {
            if (MirrorStage1NS)
            {
                paths[2].SetActive(true);
                paths[2].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, -1, player, "NWDirection");
                return true;
            }
            if (MirrorStage2NNESSW)
            {
                paths[3].SetActive(true);
                paths[3].GetComponent<Renderer>().material = player;
                ContinueLaser(0, -2, player, "NDirection");
                return true;
            }
            if (MirrorStage4ENEWSW)
            {
                paths[5].SetActive(true);
                paths[5].GetComponent<Renderer>().material = player;
                ContinueLaser(1, 1, player, "SEDirection");
                return true;
            }
            if (MirrorStage5WNWESE)
            {
                paths[0].SetActive(true);
                paths[0].GetComponent<Renderer>().material = player;
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
            if (MirrorStage7SouthSplitter) {
                paths[3].SetActive(true);
                paths[5].SetActive(true);
                paths[5].GetComponent<Renderer>().material = player;
                paths[3].GetComponent<Renderer>().material = player;
                ContinueLaser(1, 1, player, "SEDirection");
                ContinueLaser(0, -2, player, "NDirection");
                return true;
            }
        }
        else if (entry == 2)
        {
            if (MirrorStage1NS)
            {
                paths[1].SetActive(true);
                paths[1].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
            if (MirrorStage3NNWSSE)
            {
                paths[0].SetActive(true);
                paths[0].GetComponent<Renderer>().material = player;
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
            if (MirrorStage4ENEWSW)
            {
                paths[3].SetActive(true);
                paths[3].GetComponent<Renderer>().material = player;
                ContinueLaser(0, -2, player, "NDirection");
                return true;
            }
            if (MirrorStage5WNWESE)
            {
                paths[4].SetActive(true);
                paths[4].GetComponent<Renderer>().material = player;
                ContinueLaser(1, -1, player, "NEDirection");
                return true;
            }
            if (MirrorStage6NorthSplitter) {
                paths[0].SetActive(true);
                paths[4].SetActive(true);
                paths[0].GetComponent<Renderer>().material = player;
                paths[4].GetComponent<Renderer>().material = player;
                ContinueLaser(1,-1,player, "NEDirection");
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
        }
        else if (entry == 3)
        {
            if (MirrorStage2NNESSW)
            {
                paths[1].SetActive(true);
                paths[1].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
            if (MirrorStage3NNWSSE)
            {
                paths[5].SetActive(true);
                paths[5].GetComponent<Renderer>().material = player;
                ContinueLaser(1, 1, player, "SEDirection");
                return true;
            }
            if (MirrorStage4ENEWSW)
            {
                paths[2].SetActive(true);
                paths[2].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, -1, player, "NWDirection");
                return true;
            }
            if (MirrorStage5WNWESE)
            {
                paths[4].SetActive(true);
                paths[4].GetComponent<Renderer>().material = player;
                ContinueLaser(1, -1, player, "NEDirection");
                return true;
            }
            if (MirrorStage7SouthSplitter) {
                paths[1].SetActive(true);
                paths[5].SetActive(true);
                paths[1].GetComponent<Renderer>().material = player;
                paths[5].GetComponent<Renderer>().material = player;
                ContinueLaser(1, 1, player, "SEDirection");
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
        }
        else if (entry == 4)
        {
            if (MirrorStage1NS)
            {
                paths[5].SetActive(true);
                paths[5].GetComponent<Renderer>().material = player;
                ContinueLaser(1, 1, player, "SEDirection");
                return true;
            }
            if (MirrorStage2NNESSW)
            {
                paths[0].SetActive(true);
                paths[0].GetComponent<Renderer>().material = player;
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
            if (MirrorStage4ENEWSW)
            {
                paths[2].SetActive(true);
                paths[2].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, -1, player, "NWDirection");
                return true; // FIX BROKE
            }
            if (MirrorStage5WNWESE)
            {
                paths[3].SetActive(true);
                paths[3].GetComponent<Renderer>().material = player;
                ContinueLaser(0, -2, player, "NDirection");
                return true;
            }
            if (MirrorStage6NorthSplitter) {
                paths[0].SetActive(true);
                paths[2].SetActive(true);
                paths[0].GetComponent<Renderer>().material = player;
                paths[2].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, -1, player, "NWDirection");
                ContinueLaser(0, 2, player, "SDirection");
                return true;
            }
        }
        else if (entry == 5)
        {
            if (MirrorStage1NS)
            {
                paths[4].SetActive(true);
                paths[4].GetComponent<Renderer>().material = player;
                ContinueLaser(1, -1, player, "NEDirection");
                return true;
            }
            if (MirrorStage3NNWSSE)
            {
                paths[3].SetActive(true);
                paths[3].GetComponent<Renderer>().material = player;
                ContinueLaser(0, -2, player, "NDirection");
                return true;
            }
            if (MirrorStage4ENEWSW)
            {
                paths[5].SetActive(true);
                paths[5].GetComponent<Renderer>().material = player;
                ContinueLaser(1, 1, player, "SEDirection");
                return true;
            }
            if (MirrorStage5WNWESE)
            {
                paths[1].SetActive(true);
                paths[1].GetComponent<Renderer>().material = player;
                ContinueLaser(-1, 1, player, "SWDirection");
                return true;
            }
            if (MirrorStage7SouthSplitter) {
                paths[1].SetActive(true);
                paths[3].SetActive(true);
                paths[1].GetComponent<Renderer>().material = player;
                paths[3].GetComponent<Renderer>().material = player;
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




}