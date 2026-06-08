using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed    = 7f; // 이동 속도
    [SerializeField] private float rotateSpeed  = 7f; // 회전 속도

    public void MoveToTarget(Transform target)
    {        
        Vector3 moveDir = target.position - transform.position;
        moveDir.y = 0;

        // 타겟과의 거리가 너무 가까우면 멈추게 설정
        if (moveDir.sqrMagnitude < 0.001f)
        {
            Debug.Log("너무 가까워서 움직임 멈춤");
            return;
        }

        // Vector3 정규화
        moveDir = moveDir.normalized;

        RotateToward(moveDir);

        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);  
    }

    private void RotateToward(Vector3 dir)
    {
        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);

        // 부드러운 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    // #TODO: 사용할지 안할지 모름
    public void LookAtTarget(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude < 0.001f) return;

        RotateToward(dir.normalized);
    }
}
