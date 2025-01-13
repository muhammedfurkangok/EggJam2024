using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalScale;
    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanBeClicked canBeClicked;
    private bool isDragging;

    [SerializeField] private Vector2 hoverOffset = new Vector2(0, 20f);
    [SerializeField] private float hoverDuration = 0.3f;

    private void Start()
    {
       
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
        canBeClicked = GetComponent<CanBeClicked>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursor(CursorManager.Instance.hoverCursor, Vector2.zero);
        transform.DOLocalMove(new Vector2(transform.localPosition.x, originalPosition.y) + hoverOffset, hoverDuration)
            .SetEase(Ease.OutQuad);
        transform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.SetCursor(CursorManager.Instance.defaultCursor, Vector2.zero);
        if(isDragging)
        {
            return;
        }
        transform.DOLocalMove(new Vector2(transform.localPosition.x, originalPosition.y), hoverDuration)
            .SetEase(Ease.InQuad);
        transform.localScale = originalScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canBeClicked.isTabOpen)
        {
            canBeClicked.isTabOpen = true;
        }

        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canBeClicked.isTabOpen)
        {
            canBeClicked.isTabOpen = false;
        }
        if (eventData.pointerDrag != null)
        {
            RectTransform dropArea = eventData.pointerEnter.GetComponent<RectTransform>();
            if (dropArea.CompareTag("App") && dropArea != rectTransform)
            {
                Debug.Log("Dropped on another object, perform the action.");
            }
        }
    }
    
}