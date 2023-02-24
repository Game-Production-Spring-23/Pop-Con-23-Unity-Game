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
            for (int j = 0; j < RowHeight; j++)
            {
                gridArray[i, j] = (Tile)Instantiate(GridTile, new Vector2(i * 2.0f, j * 2.0f), Quaternion.identity);
                gridArray[i, j].x_cord = i;
                gridArray[i, j].y_cord = j;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
