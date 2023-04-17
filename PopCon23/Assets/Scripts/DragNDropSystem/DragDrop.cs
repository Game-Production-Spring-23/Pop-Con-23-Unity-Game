using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{

    [SerializeField]
    public Canvas canvas;

    //variable to snap back the 
    private Vector3 startingPosition;

    //int to determine if this item belongs to player 1 or 2
    public int playersItem;

    //int to identify which item this is for the player
    public int itemID;

    

    
    void Start()
    {
        startingPosition = this.transform.position;

        
        
    }



    public void DragHandler(BaseEventData data)
    {

        //only drag the item if it is this players turn
        if(TurnBasedSystem.playerTurn == this.playersItem && TurnBasedSystem.isHovering == false && TurnBasedSystem.canDrag == true)
        {
            PointerEventData pointerData = (PointerEventData)data;
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                canvas.worldCamera,
                out position);

            transform.position = canvas.transform.TransformPoint(position);
        }
        
    }

    public void BeginDraggingMethod()
    {
        if(TurnBasedSystem.playerTurn == this.playersItem && TurnBasedSystem.isHovering == false && TurnBasedSystem.canDrag == true)
        {
            //indicate that they are dragging an item
            TurnBasedSystem.draggingItem = true;

            //let the game know which item is being dragged
            TurnBasedSystem.currentItem = itemID;

            TurnBasedSystem.draggedItem = this;
        }
        

    }

    //method to be called when we stop dragging the object
    public void EndDragMethod()
    {
        //indicate that nobody is dragging an item anymore
        TurnBasedSystem.draggingItem = false;



        if(TurnBasedSystem.isHovering == false)
        {
            //I am setting it up so whenever you stop dragging the object, it will return to it's placeholder spot
            this.transform.position = startingPosition;
        }
        
    }

    public void ReplaceItem()
    {
        this.transform.position = startingPosition;
    }


}