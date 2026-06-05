using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private Vector2Int playerStartPoint;

    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform target;
    [SerializeField] private int monsterCount = 5;
    [SerializeField] private float spawnY = 1f;

    // 생성된 몬스터를 List로 관리
    private List<GameObject> _monsters = new List<GameObject>();
    public List<GameObject> Monsters => _monsters;

    private void Start() => SpawnAll();
    public void SpawnAll()
    {
        ClearAll();

        for (int i = 0; i < monsterCount; i++)
        {
            
        }
    }

    private void ClearAll()
    {
        foreach (var mon in _monsters)
        {
            if (mon != null) Destroy(mon);
        }

        _monsters.Clear();
    }
}
