using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour
{
    public void ConfirmClick()
    {
        //check to make sure that an item is hovering over a tile
        if(TurnBasedSystem.isHovering == true)
        {
            TurnBasedSystem.confirmAction = true;

            TurnBasedSystem.resetItem = true;

            TurnBasedSystem.nextTurn = true;

            TurnBasedSystem.canDrag = false;
        }
    }

    //method to undo the action from the user
    public void UndoClick()
    {
        if(TurnBasedSystem.isHovering == true)
        {
            TurnBasedSystem.isHovering = false;
            TurnBasedSystem.resetItem = true;
        }
    }

    //method to end the turn
    public void EndTurn()
    {
        if(TurnBasedSystem.nextTurn == true)
        {
            TurnBasedSystem.turnNumber++;
            TurnBasedSystem.nextTurn = false;
            if (GameManager.instance.Player1Turn == true){
                GameManager.instance.gridArray[4,2].ShieldsOn();
                GameManager.instance.Player1Turn = false;
            }
            else {
                GameManager.instance.gridArray[8,10].ShieldsOn();
                GameManager.instance.Player1Turn = true;
            }
            TurnBasedSystem.canDrag = true;


            if(TurnBasedSystem.turnNumber % 2 == 1)
            {
                TurnBasedSystem.beginNextTurn = true;
            }
            
        }
        
    }
}
