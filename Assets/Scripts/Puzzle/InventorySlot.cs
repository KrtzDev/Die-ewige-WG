using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public PuzzleObject puzzleObject;

    [SerializeField]
    private Image _image;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void UpdateSprite(Sprite sprite)
    {
        _image.sprite = sprite;
        if (sprite)
        {
            _image.color = Color.white;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (puzzleObject)
        {
            _image.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (puzzleObject)
        {
            if (!eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out InventorySlot inventorySlot))
            {
                _image.transform.position = _startPosition;
                _image.color = new Color(1,1,1,0);
                puzzleObject.RemoveFromInventory();
                puzzleObject.CheckForSolutionObject();
            }
        }
    }


}
