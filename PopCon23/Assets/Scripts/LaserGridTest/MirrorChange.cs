using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorChange : MonoBehaviour
{
    [SerializeField] public static Tile Tile;
    public Material material;
    public bool ColorChangebool;

    

   

    // Update is called once per frame
    void Update()
    {
        if (Tile == null){
            return;
        }
        if(Tile.usedRock == true)
        {
            UseRock();
            
        }

        if(Tile.usedRotator == true)
        {
            Rotate();
        }

        //remove the blocker after a set amount of turns
        if (Tile.hasBlocker == true && ((TurnBasedSystem.turnNumber - Tile.blockerTurn) > 3))
        {
            Tile.hasBlocker = false;
            Tile.MirrorStage9Blocker = false;
            Tile.MirrorStage9BlockerObject.SetActive(false);
            Debug.Log("Removing the blocker!");
            GameManager.instance.LaserFire();


        }

    }

    //Changing the OnMouseDown method so that it only displays items if the player has placed one of their own onto the tile
    public void OnMouseDown()
    {
        if (ColorChangebool == true && Tile.personalMirror == true)
        {
            ColorChange();
        }

        else if((Tile.hasMirror == true || Tile.personalMirror == true) && ColorChangebool == false && Tile.GetTurn() == TurnBasedSystem.turnNumber) 
        {
            Tile.isEmpty = false;
            MirrorChangePosition();
        }

        else if(Tile.hasSplitter == true && ColorChangebool == false && Tile.GetTurn() == TurnBasedSystem.turnNumber)
        {
            Tile.isEmpty = false;
            SplitterChangePosition();
        }

        else if(Tile.hasBlocker == true && ColorChangebool == false)
        {
            Tile.isEmpty = false;
            Tile.MirrorStage9Blocker = true;
            DisplayBlocker();
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
           Tile.MirrorStage1NS = true;
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
       
   }

    //method used for the splitters to change their position
    public void SplitterChangePosition()
    {
        if (Tile.MirrorStage7NorthSplitter == true)
        {
            Tile.MirrorStage7NorthSplitter = false;
            Tile.MirrorStage8SouthSplitter = true;
        }
        else if (Tile.MirrorStage8SouthSplitter == true)
        {
            Tile.MirrorStage8SouthSplitter = false;
            Tile.MirrorStage7NorthSplitter = true;
        }

        else
        {
            Tile.MirrorStage7NorthSplitter = true;
        }

        DisplaySplitter();
        GameManager.instance.LaserFire();
    }

    //method to activate and display a blocker if the player has placed it there
    public void DisplayBlocker()
    {
        
        Tile.MirrorStage9BlockerObject.SetActive(true);
        GameManager.instance.LaserFire();

    }

    public void DisplaySplitter()
    {
        Tile.MirrorStage7NorthSplitterObject.SetActive(false);
        Tile.MirrorStage8SouthSplitterObject.SetActive(false);

        //Displaying the splitters
        if (Tile.MirrorStage7NorthSplitter == true)
        {
            Tile.MirrorStage7NorthSplitterObject.SetActive(true);
        }
        if (Tile.MirrorStage8SouthSplitter == true)
        {
            Tile.MirrorStage8SouthSplitterObject.SetActive(true);
        }
    }

    //rock method
    public void UseRock()
    {
        Tile.usedRock = false;
        Tile.MirrorStage1NSObject.SetActive(false);
        Tile.MirrorStage2NNESSWObject.SetActive(false);
        Tile.MirrorStage3NNWSSEObject.SetActive(false);
        Tile.MirrorStage4ENEWSWObject.SetActive(false);
        Tile.MirrorStage5WNWESEObject.SetActive(false);
        Tile.MirrorStage6EWObject.SetActive(false);
        Tile.MirrorStage7NorthSplitterObject.SetActive(false);
        Tile.MirrorStage8SouthSplitterObject.SetActive(false);
        Tile.MirrorStage9BlockerObject.SetActive(false);

        Tile.hasMirror = false;
        Tile.hasSplitter = false;
        Tile.hasBlocker = false;
        Tile.personalMirror = false;

        Tile.MirrorStage1NS = false; ;
        Tile.MirrorStage2NNESSW = false;
        Tile.MirrorStage3NNWSSE = false;
        Tile.MirrorStage4ENEWSW = false;
        Tile.MirrorStage5WNWESE = false;
        Tile.MirrorStage6EW = false;
        Tile.MirrorStage7NorthSplitter = false;
        Tile.MirrorStage8SouthSplitter = false;
        Tile.MirrorStage9Blocker = false;

        GameManager.instance.LaserFire();


    }

    //rotation method
    public void Rotate()
    {

        //in the case where a personal mirror is not present
        if(Tile.personalMirror == false)
        {
            Tile.SetTurn();
        }

        else if(Tile.personalMirror == true)
        {
            //check to see that the correct player is trying to rotate their personal mirror
            if(Tile.personalMirrorOwner == (TurnBasedSystem.turnNumber % 2))
            {
                Debug.Log("Personal Mirror Testing");
                Tile.SetTurn();
            }

            else
            {
                TurnBasedSystem.canDrag = true;
            }
        }

        Tile.usedRotator = false;
        
        
    }
    
}
