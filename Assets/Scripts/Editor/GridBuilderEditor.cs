using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridBuilder))]
public class GridBuilderEditor : Editor
{
    private GridBuilder builder => target as GridBuilder;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create"))
        {
            Build();
        }
    }

    public void Build()
    {
        for (var i = 0; i != builder.width; ++i)
        {
            var x = i - (builder.width - 1f) / 2f;
            for (var j = 0; j != builder.height; ++j)
            {
                var y = j - (builder.height - 1f) / 2f;

                var existingChild = builder.transform.Find(SquareName(i, j));
                GridSquare square = null;
                if (existingChild == null)
                {
                    square = PrefabUtility.InstantiatePrefab(builder.GridSquarePrefab, builder.transform) as GridSquare;
                    square.gameObject.name = SquareName(i, j);
                } else
                {
                    square = existingChild.GetComponent<GridSquare>();
                }

                square.transform.localPosition = new Vector3(x * builder.size.x, 0, y * builder.size.y);
                var ground = square.transform.Find("Ground").transform;
                ground.localScale = new Vector3(builder.size.x, ground.localScale.y, builder.size.y);
            }
        }
    }

    private string SquareName(int i, int j)
    {
        return $"Square {i}-{j}";
    }
}
