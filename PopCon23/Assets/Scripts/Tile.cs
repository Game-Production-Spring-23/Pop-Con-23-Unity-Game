using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x_cord;
    public int y_cord;

    public GameObject NPath;
    public GameObject EPath;
    public GameObject SPath;
    public GameObject WPath;

    public Material Player1Color;
    public Material Player2Color;
    public Material Player3Color;
    public Material Player4Color;

    public bool NWSEMirror;
    public bool NESWMirror;

    public LaserTestPush NButton;
    public LaserTestPush EButton;
    public LaserTestPush SButton;
    public LaserTestPush WButton;
    void Start()
    {
        NButton.tile = this;
        EButton.tile = this;
        SButton.tile = this;
        WButton.tile = this;
        NButton.x_cord = x_cord;
        EButton.x_cord = x_cord;
        SButton.x_cord = x_cord;
        WButton.x_cord = x_cord;
        NButton.y_cord = y_cord;
        EButton.y_cord = y_cord;
        SButton.y_cord = y_cord;
        WButton.y_cord = y_cord;
    }

    public void StartLaser(string player, string direction)
    {
        //Begins Direction Checking
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
    }
    //Tells Tile Next door according to where the laser comes out to start their laser script
    public void ContinueLaser(int x, int y, string player, string direction)
    {
        GameManager.instance.gridArray[x_cord - x, y_cord - y].StartLaser(player, direction);
    }

    public void LaserReflect()
    {
    }




}
