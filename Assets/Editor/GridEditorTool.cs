using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class GridEditorTool : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager gridManager = (GridManager)target;

        if (GUILayout.Button("Create Grid"))
        {
            gridManager.CreateGrid();
        }
         if (GUILayout.Button("Clear Grid"))
        {
            gridManager.ClearGrid();
        }
    }
}
