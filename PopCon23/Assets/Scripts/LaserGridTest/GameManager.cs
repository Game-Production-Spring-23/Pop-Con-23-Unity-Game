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

    // Start is called before the first frame update
    void Start()
    {
        gridArray = new Tile[ColumnLength, RowHeight];
        for (int i = 0; i < ColumnLength; i++)
        {
            if (i % 2 == 0) {
                for (int j = 0; j < RowHeight; j = j + 2)
                {
                    gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
                    gridArray[i, j].x_cord = i;
                    gridArray[i, j].y_cord = j;
                }
            }
            else
            {
                for (int j = 1; j < RowHeight; j = j + 2)
                {
                    gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * tileWidth, j * tileHeight), Quaternion.identity);
                    gridArray[i, j].x_cord = i;
                    gridArray[i, j].y_cord = j;
                }
            }

            //       gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * 3f, j * 2.0f), Quaternion.identity);

            //         gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * 1.675f + 1.675f, j * 1.1f), Quaternion.identity);
        }
        LaserFire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaserFire()
    {
        for (int i = 0; i < ColumnLength; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < RowHeight; j = j + 2)
                {
                    gridArray[i, j].NWPath.SetActive(false);
                    gridArray[i, j].NEPath.SetActive(false);
                    gridArray[i, j].SWPath.SetActive(false);
                    gridArray[i, j].SEPath.SetActive(false);
                    gridArray[i, j].NPath.SetActive(false);
                    gridArray[i, j].SPath.SetActive(false);
                }
            }
            else
            {
                for (int j = 1; j < RowHeight; j = j + 2)
                {
                    gridArray[i, j].NWPath.SetActive(false);
                    gridArray[i, j].NEPath.SetActive(false);
                    gridArray[i, j].SWPath.SetActive(false);
                    gridArray[i, j].SEPath.SetActive(false);
                    gridArray[i, j].NPath.SetActive(false);
                    gridArray[i, j].SPath.SetActive(false);
                }
            }
        }

        gridArray[8, 6].StartLaser(gridArray[8, 6].Player1Color, "NDirection");
        gridArray[0, 0].StartLaser(gridArray[0, 0].Player2Color, "SDirection");
    }
}
