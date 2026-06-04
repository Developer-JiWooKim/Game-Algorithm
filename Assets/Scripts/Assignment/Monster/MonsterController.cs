using UnityEngine;

/// <summary>
/// 몬스터의 행동을 결정하고 실행하는 컨트롤러
/// </summary>
public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private MonsterSight _monsterSight;
    private MonsterMove _monsterMove;

    private void Start() => Initialize();
    private void Initialize()
    {
        _monsterMove = gameObject.GetComponent<MonsterMove>();
        _monsterSight = gameObject.GetComponent<MonsterSight>();
    }
    private void Update()
    {
        if(_monsterSight.TargetSense(target))
        {
            _monsterMove.MoveToTarget(target);
        }
    }

}
