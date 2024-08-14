using UnityEngine;

public class GridMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial; // Material yang digunakan saat hover
    [SerializeField] private Material defaultMaterial; // Material default
    private Renderer currentRenderer;

    void Update()
    {
        // Menggunakan raycast untuk mendeteksi objek yang terkena kursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Jika raycast mengenai objek
        if (Physics.Raycast(ray, out hit))
        {
            // Memeriksa apakah objek yang terkena raycast memiliki Renderer
            Renderer renderer = hit.collider.GetComponent<Renderer>();

            // Jika renderer ditemukan
            if (renderer != null)
            {
                // Jika renderer saat ini berbeda dari renderer sebelumnya
                if (renderer != currentRenderer)
                {
                    // Kembalikan material renderer sebelumnya ke material default
                    if (currentRenderer != null)
                    {
                        currentRenderer.material = defaultMaterial;
                    }

                    // Set material renderer yang baru
                    renderer.material = hoverMaterial;
                    currentRenderer = renderer;
                }
            }
        }
        else
        {
            // Jika raycast tidak mengenai objek, kembalikan material ke default
            if (currentRenderer != null)
            {
                currentRenderer.material = defaultMaterial;
                currentRenderer = null;
            }
        }
    }
}
