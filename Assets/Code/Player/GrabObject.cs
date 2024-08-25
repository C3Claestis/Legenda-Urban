using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public bool isCanGrab;
    public bool isGrab;
    private Transform reference;
    private Rigidbody rb;

    public void SetIsCanGrab(bool isCanGrab)
    {
        this.isCanGrab = isCanGrab;
    }

    public void SetReference(Transform reference)
    {
        this.reference = reference;
    }

    public void ToggleGrab()
    {
        isGrab = !isGrab;
        HandleRigidbody();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isCanGrab && isGrab)
        {
            transform.parent = reference;
            transform.position = reference.position;
        }
        else if (!isGrab)
        {
            isCanGrab = false;
            transform.parent = null;
        }
    }

    private void HandleRigidbody()
    {
        if (isGrab)
        {
            if (rb != null)
            {
                Destroy(rb);
            }
        }
        else
        {
            if (rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}
