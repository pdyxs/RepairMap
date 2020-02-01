using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridSquare))]
public class GridSquareEditor : Editor
{
    public GridSquare square => target as GridSquare;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (EditorApplication.isPlaying)
        {
            if (!square.isSelected && GUILayout.Button("Select"))
            {
                square.grid.SelectSquare(square);
            }
        }
    }
}
