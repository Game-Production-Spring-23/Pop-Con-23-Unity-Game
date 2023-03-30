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

            //       gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * 3f, j * 2.0f), Quaternion.identity);

            //         gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * 1.675f + 1.675f, j * 1.1f), Quaternion.identity);
        }
        Invoke("LaserFire", 5);
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
                    gridArray[i, j].paths[5].SetActive(false);
                    gridArray[i, j].paths[1].SetActive(false);
                    gridArray[i, j].paths[4].SetActive(false);
                    gridArray[i, j].paths[2].SetActive(false);
                    gridArray[i, j].paths[0].SetActive(false);
                    gridArray[i, j].paths[3].SetActive(false);
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
                }
            }
        }

        gridArray[10, 12].StartLaser(gridArray[10, 12].Player1Color, "NDirection");
        gridArray[2, 0].StartLaser(gridArray[2, 0].Player2Color, "SDirection"); 
    }
}
