using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Vector3 offset;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(null);
        offset = transform.position - GetWorldPosition(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetWorldPosition(eventData.position) + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out hit))
        {
            Transform target = hit.transform;
            if (target.CompareTag("GridCell")) // Ensure the tag is assigned to grid cells
            {
                transform.SetParent(target);
                transform.position = target.position;
            }
            else
            {
                transform.SetParent(originalParent);
            }
        }
        else
        {
            transform.SetParent(originalParent);
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
