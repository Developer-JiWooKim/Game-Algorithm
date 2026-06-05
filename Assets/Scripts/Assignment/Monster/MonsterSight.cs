using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    [SerializeField] private float detectionRange = 7f;   // 감지 반경
    [SerializeField] private float fieldOfView    = 60f;   // 전체 시야각
    [SerializeField] private bool  isSense        = false; // 감지 여부

    public bool TargetSense(Transform target)
    {
        if (target == null) return isSense = false;

        Vector3 dirToPlayer = target.position - transform.position;
        dirToPlayer.y = 0;

        // 타겟이 범위(현재 10f) 
        if (dirToPlayer.magnitude > detectionRange)
        {
            return isSense = false;
        }

        float dot = Vector3.Dot(transform.forward, dirToPlayer.normalized);    
        dot = Mathf.Clamp(dot, -1, 1);

        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        
        // 시야각 안으로 플레이어가 들어오면 감지 성공
        return isSense = (angle < fieldOfView * 0.5f);
    }

    private void OnDrawGizmos()
    {     
        // 감지 반경
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 시야각 경계선 (회전 행렬로 좌/우 벡터 계산)
        Vector3 fwd = transform.forward;
        float halfRad = fieldOfView * 0.5f * Mathf.Deg2Rad;

        Vector3 leftDir = new Vector3(
             fwd.x * Mathf.Cos(-halfRad) - fwd.z * Mathf.Sin(-halfRad), 0,
             fwd.x * Mathf.Sin(-halfRad) + fwd.z * Mathf.Cos(-halfRad));

        Vector3 rightDir = new Vector3(
             fwd.x * Mathf.Cos(halfRad) - fwd.z * Mathf.Sin(halfRad), 0,
             fwd.x * Mathf.Sin(halfRad) + fwd.z * Mathf.Cos(halfRad));

        Gizmos.color = isSense ? Color.red : Color.yellow;
        Gizmos.DrawRay(transform.position, leftDir * detectionRange);
        Gizmos.DrawRay(transform.position, rightDir * detectionRange);
    }
}
