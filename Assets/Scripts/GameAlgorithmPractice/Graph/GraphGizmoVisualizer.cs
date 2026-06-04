using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GraphGizmoVisualizer : MonoBehaviour
{
    [Header("Nodes")]
    [SerializeField] private Vector3[] nodePositions =
    {
        new Vector3(0f,0f,0f),
        new Vector3(2f,0f,1f),
        new Vector3(4f,0f,0f),
        new Vector3(2f,0f,-2f)        
    };

    [Header("Edges")]
    [SerializeField]
    private Vector2Int[] edges =
    {
        new Vector2Int(0,1),
        new Vector2Int(1,2),
        new Vector2Int(1,3),
        new Vector2Int(3,2)
    };

    [SerializeField] private float nodeRadius = 0.2f;

    private void OnDrawGizmos()
    {
        if (nodePositions == null) return;

        DrawEdges();
        DrawNodes();
    }

    private void DrawNodes()
    {
        for (int i = 0; i < nodePositions.Length; i++)
        {
            Vector3 worldPosition = transform.position + nodePositions[i];

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(worldPosition, nodeRadius);

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(worldPosition, nodeRadius + .04f);
        }
    }

    private void DrawEdges()
    {
        if (edges == null) return;

        Gizmos.color = Color.white;

        foreach (Vector2Int edge in edges)
        {
            if (!IsValidNodeIndex(edge.x) || !IsValidNodeIndex(edge.y)) continue;

            Vector3 from = transform.position + nodePositions[edge.x];
            Vector3 to = transform.position + nodePositions[edge.y];

            Gizmos.DrawLine(from, to);
        }
    }

    private bool IsValidNodeIndex(int index)
    {
        return index >= 0 && index < nodePositions.Length;
    }
}
