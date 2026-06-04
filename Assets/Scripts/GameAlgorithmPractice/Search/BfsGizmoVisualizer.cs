using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BfsGizmoVisualizer : MonoBehaviour
{
    [Header("Grid")]
    [Tooltip("가로 칸 수입니다.")]
    [SerializeField] private int width = 3;

    [Tooltip("세로 칸 수입니다.")]
    [SerializeField] private int height = 3;

    [Tooltip("Scene 뷰에 그릴 칸 크기입니다.")]
    [SerializeField] private float cellSize = 1f;

    private readonly Queue<Vector2Int> frontier = new Queue<Vector2Int>();
    private readonly HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
    private Vector2Int currentNode;

    private void Start() => ResetSearch();
    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StepSearch();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetSearch();
        }
    }

    private void StepSearch()
    {
        if(frontier.Count == 0) return;

        currentNode = frontier.Dequeue();

        foreach (Vector2Int neighbor in GetNeighbors(currentNode))
        {
            // 방문한 노드면 패스
            if (visited.Contains(neighbor)) continue;

            // 현재 노드를 방문기록을 남기고 큐에 추가
            visited.Add(neighbor);
            frontier.Enqueue(neighbor);
        }
    }

    private List<Vector2Int> GetNeighbors(Vector2Int node)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,            
        };

        foreach (Vector2Int direction in directions)
        {
            Debug.Log($"BfsGizmoVisualizer current Node : {node}");
            Vector2Int next = node + direction;
            Debug.Log($"BfsGizmoVisualizer next Node : {next}");
            if (next.x < 0 || next.x >= width || next.y < 0 || next.y >= height)
            {
                Debug.Log($"BfsGizmoVisualizer next Node : {next} => 방문 불가(방문할 영역 미만 혹은 초과)");
                continue;
            }

            neighbors.Add(next);            
        }
        Debug.Log($"Step 종료");
        return neighbors;
    }

    private void ResetSearch()
    {
        frontier.Clear();
        visited.Clear();

        // 0,0번째 노드(시작 노드)로 초기화
        currentNode = new Vector2Int(0,0);
        frontier.Enqueue(currentNode);
        visited.Add(currentNode);
    }

    private void OnDrawGizmos()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2Int node = new Vector2Int(x, y);
                Vector3 position = transform.position + new Vector3(x * cellSize, 0f, y * cellSize);

                Gizmos.color = GetNodeColor(node);
                Gizmos.DrawCube(position, Vector3.one * (cellSize * 0.8f));

                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(position, Vector3.one * (cellSize * 0.8f));
            }
        }
    }

    private Color GetNodeColor(Vector2Int node)
    {
        if (Application.isPlaying && node == currentNode)
        {
            return Color.yellow;
        }

        if (Application.isPlaying && visited.Contains(node))
        {
            return Color.cyan;
        }

        return Color.gray;
    }
}
