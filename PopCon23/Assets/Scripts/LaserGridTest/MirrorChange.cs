using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorChange : MonoBehaviour
{
    public Tile Tile;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        MirrorChangePosition();
    }
    public void MirrorChangePosition()
    {
        if (Tile.MirrorStage1NS == true)
        {
            Tile.MirrorStage1NS = false;
            Tile.MirrorStage2NNESSW = true;
        }
        else if (Tile.MirrorStage2NNESSW == true)
        {
            Tile.MirrorStage2NNESSW = false;
            Tile.MirrorStage3NNWSSE = true;
        }
        else if (Tile.MirrorStage3NNWSSE == true)
        {
            Tile.MirrorStage3NNWSSE = false;
            Tile.MirrorStage4ENEWSW = true;
        }
        else if (Tile.MirrorStage4ENEWSW == true)
        {
            Tile.MirrorStage4ENEWSW = false;
            Tile.MirrorStage5WNWESE = true;
        }
        else if (Tile.MirrorStage5WNWESE == true)
        {
            Tile.MirrorStage5WNWESE = false;
        }
        else
        {
            Tile.MirrorStage1NS = true;

        }
        DisplayMirror();
    }
    public void DisplayMirror()
    {
        Tile.MirrorStage1NSObject.SetActive(false);
        Tile.MirrorStage2NNESSWObject.SetActive(false);
        Tile.MirrorStage3NNWSSEObject.SetActive(false);
        Tile.MirrorStage4ENEWSWObject.SetActive(false);
        Tile.MirrorStage5WNWESEObject.SetActive(false);

        if (Tile.MirrorStage1NS == true)
        {
            Tile.MirrorStage1NSObject.SetActive(true);
        }
        if (Tile.MirrorStage2NNESSW == true)
        {
            Tile.MirrorStage2NNESSWObject.SetActive(true);
        }
        if (Tile.MirrorStage3NNWSSE == true)
        {
            Tile.MirrorStage3NNWSSEObject.SetActive(true);
        }
        if (Tile.MirrorStage4ENEWSW == true)
        {
            Tile.MirrorStage4ENEWSWObject.SetActive(true);
        }
        if (Tile.MirrorStage5WNWESE == true)
        {
            Tile.MirrorStage5WNWESEObject.SetActive(true);
        }
    }
}
