using UnityEngine;

/// <summary>
/// 몬스터의 행동을 결정하고 실행하는 컨트롤러
/// </summary>
public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform target;

    private MonsterSight _monsterSight;
    private MonsterMove _monsterMove;

    private bool _isSense = false;

    private void Start() => Initialize();
    private void Initialize()
    {
        _monsterMove  = GetComponent<MonsterMove>();
        _monsterSight = GetComponent<MonsterSight>();
    }
    private void Update()
    {
        _isSense = _monsterSight.TargetSense(target);
        Debug.Log(_isSense);
        if (_isSense)
        {            
            _monsterMove.MoveToTarget(target);
        }
    }
}
