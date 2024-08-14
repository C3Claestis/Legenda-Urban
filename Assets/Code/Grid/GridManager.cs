using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1f;

    public void CreateGrid()
    {
        // Hapus grid yang ada sebelumnya
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        // Hitung offset agar grid terpusat di sekitar GameObject
        float startX = -columns * cellSize / 2 + cellSize / 2;
        float startZ = -rows * cellSize / 2 + cellSize / 2;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Hitung posisi cell berdasarkan offset
                Vector3 position = new Vector3(startX + i * cellSize, 0, startZ + j * cellSize);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.name = $"Cell_{i}_{j}";
                cell.transform.SetParent(transform);
            }
        }
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
