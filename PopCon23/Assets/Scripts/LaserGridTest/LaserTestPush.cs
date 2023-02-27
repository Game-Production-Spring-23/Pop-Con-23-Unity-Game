using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTestPush : MonoBehaviour
{
    public int x_cord;
    public int y_cord;
    public Tile tile;

    public string player;
    public string direction;

    void OnMouseDown()
    {
        tile.StartLaser(player, direction);
    }
}
