using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridBuilder))]
public class GridBuilderEditor : Editor
{
    private GridBuilder builder => target as GridBuilder;
    private Grid grid => builder.grid;

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
        grid.squares = new Grid.GridRow[builder.height];
        for (var i = 0; i != builder.height; ++i)
        {
            grid.squares[i] = new Grid.GridRow(builder.width);
            var y = -(i - (builder.height - 1f) / 2f);
            for (var j = 0; j != builder.width; ++j)
            {
                var x = j - (builder.width - 1f) / 2f;

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
                grid.squares[i][j] = square;
                square.x = j;
                square.y = i;

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
