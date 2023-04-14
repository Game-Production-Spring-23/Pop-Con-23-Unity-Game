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

    public Item InventoryItem;
    public Item[] Player1Inventory;
    public Item[] Player2Inventory;

    public bool Player1Turn;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FillInventory();
        FillGrid();
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
        gridArray[8,10].RedCrystal = true;
        gridArray[8,10].CrystalGeneration();
        gridArray[4,2].BlueCrystal = true;
        gridArray[4,2].CrystalGeneration();
        gridArray[10, 12].StartLaser(gridArray[10, 12].Player1Color, "NDirection");
        gridArray[2, 0].StartLaser(gridArray[2, 0].Player2Color, "SDirection"); 
    }

    public void FillGrid(){
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
    }

    public void FillInventory(){
        Player1Inventory = new Item[5];
        Player2Inventory = new Item[5];
        for (int i = 0; i < 5; i++){
            float number = Random.Range(0f,1f);
            Player1Inventory[i] = (Item)Instantiate(InventoryItem);
            Player2Inventory[i] = (Item)Instantiate(InventoryItem);
            switch (number){
                case > .9f:
                    Player1Inventory[i].Name = ".9";
                    Player2Inventory[i].Name = ".9";
                    break;
                case > .75f:
                    Player1Inventory[i].Name = ".75 - .89";
                    Player2Inventory[i].Name = ".75 - .89";
                    break;
                case > .50f:
                    Player1Inventory[i].Name = ".50 - .74";
                    Player2Inventory[i].Name = ".50 - .74";
                    break;
                case > .05f:
                    Player1Inventory[i].Name = ".05 - .49";
                    Player2Inventory[i].Name = ".05 - .49";
                    break;
                default:
                    Player1Inventory[i].Name = ".01 - .04";
                    Player2Inventory[i].Name = ".01 - .04";
                    break;
            }
        }
    }
}
