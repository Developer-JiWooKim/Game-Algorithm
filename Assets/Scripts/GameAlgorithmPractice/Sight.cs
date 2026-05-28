using UnityEngine;

public class Sight : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float sightAngle = 30f;

    private float detectionRange = 10f;  // 시야 범위

    // Update is called once per frame
    void Update()
    {
        TargetSense();
    }
    private void TargetSense()
    {
        Vector3 dirToPlayer = target.position - transform.position;

        if (dirToPlayer.magnitude > detectionRange)
        {
            Debug.Log("시야 범위 안에 타겟이 없음");
            return;
        }

        float dot = Vector3.Dot(transform.forward, dirToPlayer.normalized);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg; // Acos(아크 코사인) 과 Rad2Deg(라디안을 도(Degree)로 바꾸기 위해 곱해줄 값)을 곱해서 각도 추출

        if (angle < sightAngle) // 타겟이 시야각 안으로 들어오면
        {
            Debug.Log("타겟 발견");

            Vector3 cross = Vector3.Cross(transform.forward, dirToPlayer.normalized);
            if (cross.y > 0)
            {
                Debug.Log("타겟이 내 기준 오른쪽에 있음");
            }
            else
            {
                Debug.Log("타겟이 내 기준 왼쪽에 있음");
            }
        }
        else
        {
            Debug.Log("시야각 안에 타겟이 없음");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // 범위 원
    }
}
