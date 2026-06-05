using UnityEngine;

/// <summary>
/// 몬스터의 행동을 결정하고 실행하는 컨트롤러
/// </summary>
public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private MonsterSight _monsterSight;
    private MonsterMove  _monsterMove;
    private MonsterFSM   _monsterFSM;

    private bool isSensed = false;

    private void Start() => Initialize();
    private void Initialize()
    {
        _monsterMove    = GetComponent<MonsterMove>();
        _monsterSight   = GetComponent<MonsterSight>();
        _monsterFSM     = GetComponent<MonsterFSM>();
    }

    private void FixedUpdate()
    {
        isSensed = _monsterSight.TargetSense(target);
    }

    private void Update()
    {
        _monsterFSM.Evaluate(isSensed, target);

        MonsterAI();
    }

    private void MonsterAI()
    {
        switch (_monsterFSM.Current)
        {
            case MonsterFSM.State.Chase:
                _monsterMove.MoveToTarget(target);
                break;
            case MonsterFSM.State.Attack:
                // #TODO: 타겟이 범위 안에 들어오면 할 행동 -> OnTriggerEnter로 처리해도될듯?
                break;
        }
    }
}
