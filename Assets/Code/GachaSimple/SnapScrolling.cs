using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform contentPanel;
    [SerializeField] float snapSpeed = 10f;

    private float[] positions;
    private int currentIndex;
    private bool isLerping = false;
    private float targetPosition;

    void Start()
    {
        int childCount = contentPanel.childCount;
        positions = new float[childCount];
        float step = 1f / (childCount - 1);

        for (int i = 0; i < childCount; i++)
        {
            positions[i] = step * i;
        }

        scrollRect.horizontalNormalizedPosition = positions[0];
        currentIndex = 0;
    }

    void Update()
    {
        // Selalu panggil SnapToNearest kecuali saat lerping
        if (!isLerping)
        {
            SnapToNearest();
        }

        if (isLerping)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetPosition, snapSpeed * Time.deltaTime);

            if (Mathf.Abs(scrollRect.horizontalNormalizedPosition - targetPosition) < 0.001f)
            {
                scrollRect.horizontalNormalizedPosition = targetPosition;
                isLerping = false;
            }
        }
    }

    public void MoveContentRight()
    {
        if (currentIndex < positions.Length - 1)
        {
            currentIndex++;
        }

        targetPosition = positions[currentIndex];
        isLerping = true;
    }

    public void MoveContentLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }

        targetPosition = positions[currentIndex];
        isLerping = true;
    }

    private void SnapToNearest()
    {
        float nearestPosition = float.MaxValue;
        for (int i = 0; i < positions.Length; i++)
        {
            float distance = Mathf.Abs(scrollRect.horizontalNormalizedPosition - positions[i]);
            if (distance < nearestPosition)
            {
                nearestPosition = distance;
                currentIndex = i;
            }
        }

        targetPosition = positions[currentIndex];

        // Snap ke posisi terdekat jika tidak sedang lerping
        if (!isLerping)
        {
            scrollRect.horizontalNormalizedPosition = targetPosition;
        }
    }
}
