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

    public bool NSMirror;
    public bool NESWMirror;
    public bool NWSEMirror;
    public bool EWMirror;

    void Start()
    {

    }

    public void StartLaser(Material player, GameObject direction)
    {
        if (direction == SEPath)
        {
            Debug.Log("TEST");
            SEPath.GetComponent<Renderer>().material = Player1Color;
            NWPath.GetComponent<Renderer>().material = Player1Color;
            // IF SW PATH IS LAST PATH, CALL HEX SW OF CURRENT HEX
            ContinueLaser(1, 1, Player1Color, NWPath);
        }
        if (direction == SWPath)
        {
            Debug.Log("TEST");
            SWPath.GetComponent<Renderer>().material = Player1Color;
            NEPath.GetComponent<Renderer>().material = Player1Color;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(-1, 1, Player1Color, SWPath);
        }
        if (direction == NEPath)
        {
            Debug.Log("TEST");
            NEPath.GetComponent<Renderer>().material = Player1Color;
            SWPath.GetComponent<Renderer>().material = Player1Color;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(1, -1, Player1Color, NEPath);
        }
        if (direction == NWPath)
        {
            Debug.Log("TEST");
            NWPath.GetComponent<Renderer>().material = Player1Color;
            SEPath.GetComponent<Renderer>().material = Player1Color;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(-1, -1, Player1Color, NWPath);
        }
        if (direction == NPath)
        {
            Debug.Log("TEST");
            NPath.GetComponent<Renderer>().material = Player1Color;
            SPath.GetComponent<Renderer>().material = Player1Color;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(0, 2, Player1Color, NPath);
        }
        if (direction == SPath)
        {
            Debug.Log("TEST");
            SPath.GetComponent<Renderer>().material = Player1Color;
            NPath.GetComponent<Renderer>().material = Player1Color;
            // IF NE PATH IS LAST PATH, CALL HEX NE OF CURRENT HEX
            ContinueLaser(0, -2, Player1Color, NPath);
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
    public void ContinueLaser(int x, int y, Material player, GameObject direction)
    {
        GameManager.instance.gridArray[x_cord + x, y_cord + y].StartLaser(player, direction);
    }

    public void LaserReflect()
    {
    }




}
