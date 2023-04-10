using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorChange : MonoBehaviour
{
    public Tile Tile;
    public Material material;
    public bool ColorChangebool;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        if (ColorChangebool == true){
            ColorChange();
        }
        else {
            MirrorChangePosition();
        }
    }
    public void ColorChange(){
        Tile.MirrorStage1NSObject.GetComponent<Renderer>().material = material;
        Tile.MirrorStage2NNESSWObject.GetComponent<Renderer>().material = material;
        Tile.MirrorStage3NNWSSEObject.GetComponent<Renderer>().material = material;
        Tile.MirrorStage4ENEWSWObject.GetComponent<Renderer>().material = material;
        Tile.MirrorStage5WNWESEObject.GetComponent<Renderer>().material = material;
        Tile.MirrorStage6EWObject.GetComponent<Renderer>().material = material;
        Tile.MirrorStage9BlockerObject.GetComponent<Renderer>().material = material;
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
            Tile.MirrorStage6EW = true;
        }
        else if (Tile.MirrorStage6EW == true){
            Tile.MirrorStage6EW = false;
            Tile.MirrorStage7NorthSplitter = true;
        }
        else if (Tile.MirrorStage7NorthSplitter == true)
        {
            Tile.MirrorStage7NorthSplitter = false;
            Tile.MirrorStage8SouthSplitter = true;
        }
        else if (Tile.MirrorStage8SouthSplitter == true)
        {
            Tile.MirrorStage8SouthSplitter = false;
            Tile.MirrorStage9Blocker = true;
        }
        else if (Tile.MirrorStage9Blocker == true)
        {
            Tile.MirrorStage9Blocker = false;

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
        Tile.MirrorStage6EWObject.SetActive(false);
        Tile.MirrorStage7NorthSplitterObject.SetActive(false);
        Tile.MirrorStage8SouthSplitterObject.SetActive(false);
        Tile.MirrorStage9BlockerObject.SetActive(false);

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
        if (Tile.MirrorStage6EW == true)
        {
            Tile.MirrorStage6EWObject.SetActive(true);
        }
        if (Tile.MirrorStage7NorthSplitter == true)
        {
            Tile.MirrorStage7NorthSplitterObject.SetActive(true);
        }
        if (Tile.MirrorStage8SouthSplitter == true)
        {
            Tile.MirrorStage8SouthSplitterObject.SetActive(true);
        }
        if (Tile.MirrorStage9Blocker == true)
        {
            Tile.MirrorStage9BlockerObject.SetActive(true);
        }
    }
}
