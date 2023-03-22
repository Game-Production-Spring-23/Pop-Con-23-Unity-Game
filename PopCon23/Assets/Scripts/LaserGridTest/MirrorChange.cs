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
            Tile.MirrorStage6NorthSplitter = true;
        }
        else if (Tile.MirrorStage6NorthSplitter == true)
        {
            Tile.MirrorStage6NorthSplitter = false;
            Tile.MirrorStage7SouthSplitter = true;
        }
        else if (Tile.MirrorStage7SouthSplitter == true)
        {
            Tile.MirrorStage7SouthSplitter = false;
            Tile.MirrorStage8Blocker = true;
        }
        else if (Tile.MirrorStage8Blocker == true)
        {
            Tile.MirrorStage8Blocker = false;

        }
        else
        {
            Tile.MirrorStage1NS = true;

        }
        DisplayMirror();
        GameManager.instance.LaserFire();
    }
    public void DisplayMirror()
    {
        Tile.MirrorStage1NSObject.SetActive(false);
        Tile.MirrorStage2NNESSWObject.SetActive(false);
        Tile.MirrorStage3NNWSSEObject.SetActive(false);
        Tile.MirrorStage4ENEWSWObject.SetActive(false);
        Tile.MirrorStage5WNWESEObject.SetActive(false);
        Tile.MirrorStage6NorthSplitterObject.SetActive(false);
        Tile.MirrorStage7SouthSplitterObject.SetActive(false);
        Tile.MirrorStage8BlockerObject.SetActive(false);

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
        if (Tile.MirrorStage6NorthSplitter == true)
        {
            Tile.MirrorStage6NorthSplitterObject.SetActive(true);
        }
        if (Tile.MirrorStage7SouthSplitter == true)
        {
            Tile.MirrorStage7SouthSplitterObject.SetActive(true);
        }
        if (Tile.MirrorStage8Blocker == true)
        {
            Tile.MirrorStage8BlockerObject.SetActive(true);
        }
    }
}
