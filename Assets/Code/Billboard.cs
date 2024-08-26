using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform playerCamera;

    void Update()
    {
        // Arahkan canvas ke arah kamera dengan hanya mengubah rotasi di sumbu Y
        Vector3 direction = playerCamera.position - transform.position;
        direction.y = 0; // Hanya rotasi di sumbu Y

        // Jika ingin agar canvas selalu terlihat dari depan
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
