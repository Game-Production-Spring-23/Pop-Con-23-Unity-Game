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
        gridArray[0, 2].StartLaser(gridArray[0, 2].Player1Color, gridArray[0, 2].SEPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
