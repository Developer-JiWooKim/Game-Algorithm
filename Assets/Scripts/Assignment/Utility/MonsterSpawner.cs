using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private Vector2Int playerStartPoint = new Vector2Int(0, 0);

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

        var candidates = new List<Cell>();
        foreach (Cell cell in mazeGenerator.AllCells)
        {
            if (cell.col == playerStartPoint.x && cell.row == playerStartPoint.y) continue;
            candidates.Add(cell);
        }

        for (int i = candidates.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (candidates[i], candidates[j]) = (candidates[j], candidates[i]);
        }

        for (int i = 0; i < monsterCount; i++)
        {
            // Cell.worldCenter로 스폰 위치 결정 (벽 겹침 없음)
            Vector3 spawnPos = candidates[Random.Range(0, candidates.Count)].worldCenter;
            spawnPos.y = spawnY;

            GameObject monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
            monster.GetComponent<MonsterController>().Target = target;

            _monsters.Add(monster);
        }
    }
    public void RemoveMonster(GameObject monster)
    {
        if (!_monsters.Contains(monster)) return;

        _monsters.Remove(monster);
        Destroy(monster);
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
