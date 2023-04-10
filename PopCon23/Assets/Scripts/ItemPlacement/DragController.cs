using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{

    private bool _isDragActive = false;
    private Vector2 _screenPosition;

    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    public Draggable LastDragged => _lastDragged;

    void Awake()
    {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_isDragActive)
        {
            //checking to see if the object is already being dragged
            if (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }


        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }

        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if (_isDragActive)
        {
            Drag();
        }

        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    _lastDragged = draggable;
                    if (_lastDragged.canDrag == true && _lastDragged.playersItem == TurnBasedSystem.playerTurn)
                    {
                        InitDrag();
                    }

                }
            }
        }
    }

    void InitDrag()
    {
        //_lastDragged.LastPosition = _lastDragged.transform.position;
        UpdateDragStatus(true);
    }

    void Drag()
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    void Drop()
    {
        UpdateDragStatus(false);
        if (_lastDragged.validHex == false)
        {
            _lastDragged.transform.position = _lastDragged.LastPosition;
        }

        else
        {
            if (TurnBasedSystem.playerTurn == 1)
            {
                TurnBasedSystem.playerTurn = 2;
            }

            else
            {
                TurnBasedSystem.playerTurn = 1;
            }
        }


    }

    void UpdateDragStatus(bool isDragging)
    {
        _isDragActive = _lastDragged.IsDragging = isDragging;
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
    }
}
