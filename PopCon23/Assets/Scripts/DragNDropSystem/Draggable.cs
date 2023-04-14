using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    public bool validHex = false;
    public bool canDrag = true;

    //this int will be used to determine which player this item belongs to
    public int playersItem;

    public Vector3 LastPosition;
    private Collider2D _collider;
    private DragController _dragController;

    //object that tracks which tile the player is currently hovering above
    private TileController curTile;

    private GameObject _tile;

    private float _movementSpeed = 15f;
    private System.Nullable<Vector3> _movementDestination;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _dragController = FindObjectOfType<DragController>();
        LastPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        if (_movementDestination.HasValue)
        {
            if (IsDragging)
            {
                return;
            }

            if (transform.position == _movementDestination)
            {
                gameObject.layer = Layer.Default;
                _movementDestination = null;
            }

            else
            {

                if (curTile.isEmpty == true && validHex == true)
                {
                    LerpPos();
                    canDrag = false;

                    //setting the current tiles status to true to ensure that no other items can be placed in this tile
                    _tile.gameObject.tag = "DropInvalid";
                }

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Draggable collidedDraggable = other.GetComponent<Draggable>();

        if (collidedDraggable != null && _dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
            //Debug.Log("Dropped");
        }

        if (other.CompareTag("DropValid"))
        {
            validHex = true;
            _movementDestination = other.transform.position;
            curTile = other.gameObject.GetComponent<TileController>();

            _tile = other.gameObject;
        }
        else if (other.CompareTag("DropInvalid"))
        {
            Debug.Log("INVALID");
            _movementDestination = LastPosition;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        validHex = false;
        //Debug.Log("exiting");
        _movementDestination = LastPosition;

    }

    void LerpPos()
    {
        transform.position = Vector3.Lerp(transform.position, _movementDestination.Value, _movementSpeed * Time.fixedDeltaTime);
    }

}