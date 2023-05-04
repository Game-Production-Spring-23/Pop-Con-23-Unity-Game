using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Tile GridTile;
    public Tile[,] gridArray;
    public int ColumnLength;
    public int RowHeight;
    public float tileHeight;
    public float tileWidth;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        FillGrid();               // Creates hexagon grid for lasers
        Invoke("LaserFire", 5);   // Fires Lasers after 5 seconds
    }

    void Update()
    {
        if (TurnBasedSystem.curTile != null && TurnBasedSystem.curTile.createShockwave == true)
        {
            //make it so each tile near the current tile is also affected by the rock
            Debug.Log("Big rock initiate!!!!!");
            gridArray[(TurnBasedSystem.curTile.x_cord + 1), (TurnBasedSystem.curTile.y_cord + 1)].usedBigRock = true;
            gridArray[(TurnBasedSystem.curTile.x_cord - 1), (TurnBasedSystem.curTile.y_cord + 1)].usedBigRock = true;
            gridArray[(TurnBasedSystem.curTile.x_cord + 1), (TurnBasedSystem.curTile.y_cord - 1)].usedBigRock = true;
            gridArray[(TurnBasedSystem.curTile.x_cord - 1), (TurnBasedSystem.curTile.y_cord - 1)].usedBigRock = true;
            gridArray[(TurnBasedSystem.curTile.x_cord), (TurnBasedSystem.curTile.y_cord + 2)].usedBigRock = true;
            gridArray[(TurnBasedSystem.curTile.x_cord), (TurnBasedSystem.curTile.y_cord - 2)].usedBigRock = true;
            TurnBasedSystem.curTile.createShockwave = false;


        }

    }

    public void LaserFire()
    {
        for (int i = 0; i < ColumnLength; i++) // Resets grid lasers
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < RowHeight; j = j + 2)
                {
                    gridArray[i, j].paths[5].SetActive(false);
                    gridArray[i, j].paths[1].SetActive(false);
                    gridArray[i, j].paths[4].SetActive(false);
                    gridArray[i, j].paths[2].SetActive(false);
                    gridArray[i, j].paths[0].SetActive(false);
                    gridArray[i, j].paths[3].SetActive(false);
                    gridArray[i,j].CrystalHit = null;
                }
            }
            else
            {
                for (int j = 1; j < RowHeight; j = j + 2)
                {
                    gridArray[i, j].paths[5].SetActive(false);
                    gridArray[i, j].paths[1].SetActive(false);
                    gridArray[i, j].paths[4].SetActive(false);
                    gridArray[i, j].paths[2].SetActive(false);
                    gridArray[i, j].paths[0].SetActive(false);
                    gridArray[i, j].paths[3].SetActive(false);
                    gridArray[i,j].CrystalHit = null;
                }
            }
        }
        // Fires Lasers Red from North Blue fron South
        gridArray[10, 12].StartLaser(gridArray[10, 12].Player1Color, "NDirection");
        gridArray[2, 0].StartLaser(gridArray[2, 0].Player2Color, "SDirection"); 
    }

    public void FillGrid(){
        gridArray = new Tile[ColumnLength, RowHeight];
        for (int i = 0; i < ColumnLength; i++) // Creates Grid
        {
            if (i % 2 == 0) {
                for (int j = 0; j < RowHeight ; j = j + 2)
                {
                    gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
                    gridArray[i, j].x_cord = i;
                    gridArray[i, j].y_cord = j;
                }
            }
            else
            {
                for (int j = 1; j < RowHeight ; j = j + 2)
                {
                    gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
                    gridArray[i, j].x_cord = i;
                    gridArray[i, j].y_cord = j;
                }
            }
        }
        // Sets Crystals Hexs to be above other hex tiles to show shields
        Vector3 CrystalOffSet = new Vector3(0, 0, 1f);
        gridArray[8, 10].transform.position += CrystalOffSet;
        gridArray[4, 2].transform.position += CrystalOffSet;
        // Sets Crystals for each player
        gridArray[8, 10].RedCrystal = true;
        gridArray[8, 10].CrystalGeneration();
        gridArray[4, 2].BlueCrystal = true;
        gridArray[4, 2].CrystalGeneration();
    }

}
