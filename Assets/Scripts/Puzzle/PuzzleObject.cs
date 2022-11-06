using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleObject : MonoBehaviour
{
    private TimeObject _timeObject;

    private Transform _startParentTransform;

    private bool _isActive;
    private bool didHitInventorySlot = false;
    private InventorySlot _inventorySlot;

    [SerializeField] private GameObject targetObject;

    private void Awake()
    {
        _startParentTransform = transform.parent;
        _timeObject = GetComponent<TimeObject>();
        _timeObject.TimeObjectUpdate.AddListener(OnTimeObjectUpdated);
    }

    private void OnTimeObjectUpdated()
    {
        _isActive = _timeObject.currentPointInTime.snapshotData.isActiveAtTimeStamp;
    }
    private void OnMouseUp()
    {
        if (_isActive)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (var raycastResult in raycastResults)
            {
                if (raycastResult.gameObject.TryGetComponent(out InventorySlot slot))
                {
                    AddToInventory(slot);
                }
            }


            if (!didHitInventorySlot)
            {
                RemoveFromInventory();
                _inventorySlot = null;
            }
        }
    }

    public void CheckForSolutionObject()
    {
        Debug.Log("ping");

        Vector3 screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit2D[] hit2D = Physics2D.GetRayIntersectionAll(ray,100f);
        Debug.DrawRay(ray.origin,ray.direction * 100f,Color.red,10f);
        foreach (var hit in hit2D)
        {
            if (hit.collider.gameObject == targetObject)
            {
                _timeObject.Deactivate();
                targetObject.GetComponent<PuzzleSolutionObject>().Completed();
            }
        }
    }

    public void AddToInventory(InventorySlot slot)
    {
        didHitInventorySlot = true;
        _inventorySlot = slot;
        _inventorySlot.puzzleObject = this;
        this.transform.parent = _inventorySlot.transform;
        _inventorySlot.UpdateSprite(_timeObject.currentPointInTime.snapshotData.sprite);
        _timeObject.Deactivate();
    }

    public void RemoveFromInventory()
    {
        _timeObject.transform.position = _timeObject.currentPointInTime.snapshotData.goalPosition;
        if (_inventorySlot)
        {
            _inventorySlot.UpdateSprite(null);
            _timeObject.Activate();
            this.transform.parent = _startParentTransform;
        }
    }

    private void OnMouseDrag()
    {
        if (_isActive)
        {
            Vector3 screenPosition = Input.mousePosition;
            screenPosition.z = Camera.main.nearClipPlane + 5f;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            _timeObject.transform.position = worldPosition;
        }
    }
}
