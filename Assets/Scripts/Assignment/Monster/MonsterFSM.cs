using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    public enum State
    {
        Idle,       
        Chase,      
        Attack,     
        Dead        
    }

    [SerializeField] private State _current;
    public State Current => _current;

    public void Evaluate(bool isSensed, Transform target)
    {
        if (Current == State.Dead) return;

        switch (Current)
        {
            case State.Idle:
                if (isSensed) TransitionTo(State.Chase);
                break;
            case State.Chase:
                if (!isSensed) TransitionTo(State.Idle);
                break;
            case State.Attack:
                // #TODO: 범위 공격 범위 안에 들어오면 Attack 행동 후 다시 Chase로? Idle갔다가 다시 범위 체크하고 Chase로?
                break;
        }
    }

    public void SetDead() => TransitionTo(State.Dead);

    // 상태 변경
    private void TransitionTo(State next)
    {
        if (_current == next) return;

        _current = next;
    }
}
