using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    [SerializeField] private float sightAngle = 30f;

    private float detectionRange = 10f;
    private bool isSense = false;

    public bool TargetSense(Transform target)
    {
        Vector3 dirToPlayer = target.position - transform.position;

        // 타겟이 범위(현재 10f) 
        if (dirToPlayer.magnitude > detectionRange)
        {
            return isSense = false;
        }

        float dot = Vector3.Dot(transform.forward, dirToPlayer.normalized);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        // 시야각 안으로 플레이어가 들어오면 감지 성공
        if (angle < sightAngle)
        {
            return isSense = true;
        }
        else
        {
            return isSense = false;
        }
    }

    private void OnDrawGizmos()
    {
        // 시야각 경계선 (왼쪽)
        Gizmos.color = isSense ? Color.red : Color.green;
        Vector3 leftDir = Quaternion.Euler(0, -sightAngle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftDir * detectionRange);

        // 시야각 경계선 (오른쪽)
        Vector3 rightDir = Quaternion.Euler(0, sightAngle, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, rightDir * detectionRange);

        // 시야각 부채꼴 채우기
        int segments = 20;
        Vector3 prevDir = leftDir;
        for (int i = 1; i <= segments; i++)
        {
            float angle = -sightAngle + (2 * sightAngle * i / segments);
            Vector3 newDir = Quaternion.Euler(0, angle, 0) * transform.forward;
            Gizmos.DrawLine(
                transform.position + prevDir * detectionRange,
                transform.position + newDir * detectionRange
            );
            prevDir = newDir;
        }
    }
}
