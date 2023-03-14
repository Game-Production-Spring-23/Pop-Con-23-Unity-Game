using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x_cord;
    public int y_cord;

    public GameObject NWPath;
    public GameObject NEPath;
    public GameObject NPath;
    public GameObject SEPath;
    public GameObject SWPath;
    public GameObject SPath;

    public Material Player1Color;
    public Material Player2Color;

    public bool MirrorStage1NS;
    public bool MirrorStage2NNESSW;
    public bool MirrorStage3NNWSSE;
    public bool MirrorStage4ENEWSW;
    public bool MirrorStage5WNWESE;

    public GameObject MirrorStage1NSObject;
    public GameObject MirrorStage2NNESSWObject;
    public GameObject MirrorStage3NNWSSEObject;
    public GameObject MirrorStage4ENEWSWObject;
    public GameObject MirrorStage5WNWESEObject;

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
            SEPath.SetActive(true);
            NWPath.SetActive(true);
            SEPath.GetComponent<Renderer>().material = player;
            NWPath.GetComponent<Renderer>().material = player;
            // IF SW PATH IS LAST PATH, CALL HEX SW OF CURRENT HEX
            ContinueLaser(1, 1, player, "NWDirection");
        }
        else if (direction == "SWDirection")
        {
            Debug.Log("TEST");
            SWPath.SetActive(true);
            NEPath.SetActive(true);
            SWPath.GetComponent<Renderer>().material = player;
            NEPath.GetComponent<Renderer>().material = player;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(-1, 1, player, "SWDirection");
        }
        else if (direction == "NEDirection")
        {
            Debug.Log("TEST");
            NWPath.SetActive(true);
            SWPath.SetActive(true);
            NEPath.GetComponent<Renderer>().material = player;
            SWPath.GetComponent<Renderer>().material = player;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(1, -1, player, "NEDirection");
        }
        else if (direction == "NWDirection")
        {
            Debug.Log("TEST");
            SEPath.SetActive(true);
            NWPath.SetActive(true);
            NWPath.GetComponent<Renderer>().material = player;
            SEPath.GetComponent<Renderer>().material = player;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(-1, -1, player, "NWDirection");
        }
        else if (direction == "NDirection")
        {
            Debug.Log("TEST");
            NPath.SetActive(true);
            SPath.SetActive(true);
            NPath.GetComponent<Renderer>().material = player;
            SPath.GetComponent<Renderer>().material = player;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(0, -2, player, "NDirection");
        }
        else if (direction == "SDirection")
        {
            Debug.Log("TEST");
            NPath.SetActive(true);
            SPath.SetActive(true);
            SPath.GetComponent<Renderer>().material = player;
            NPath.GetComponent<Renderer>().material = player;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(0, 2, player, "SDirection");
        }
        else
        {
            Debug.Log(direction);
        }



        /* //Begins Direction Checking
         if (direction == "north" || direction == "south")
         {
             //If North Direction
             if (direction == "north")
             {
                 //Sets South Direction (North STARTS in south, etc)
                 SPath.SetActive(true);
                 SPath.GetComponent<Renderer>().material = Player1Color;
                 //If Mirror Active, Output would be WEST
                 if (NWSEMirror == true)
                 {
                     WPath.SetActive(true);
                     WPath.GetComponent<Renderer>().material = Player1Color;
                     ContinueLaser(-1, 0, "red", "west");
                 }
                 //If Mirror Active Output would be EAST
                 else if (NESWMirror == true)
                 {
                 }
                 //If No mirror Continues NORTH
                 else
                 {
                     NPath.SetActive(true);
                     NPath.GetComponent<Renderer>().material = Player1Color;
                     ContinueLaser(0, -1, "red", "north");
                 }
             }
             //If South Direction
             if (direction == "south")
             {
                 SPath.GetComponent<Renderer>().material = Player1Color;
             }
         }
         if (direction == "east" || direction == "west")
         {
             //If East
             if (direction == "east")
             {
                 EPath.GetComponent<Renderer>().material = Player1Color;
             }
             // If West
             if (direction == "west")
             {
                 WPath.SetActive(true);
                 WPath.GetComponent<Renderer>().material = Player1Color;
                 EPath.SetActive(true);
                 EPath.GetComponent<Renderer>().material = Player1Color;
                 ContinueLaser(-1, 0, "red", "west");
             }
         }
         */
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
    }

    public void LaserReflect()
    {
    }




}
