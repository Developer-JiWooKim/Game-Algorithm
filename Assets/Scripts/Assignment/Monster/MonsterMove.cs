using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    public void MoveToTarget(Transform target)
    {
        // Vector3 정규화
        Vector3 moveDir = target.position - transform.position;
        moveDir.y = 0;
        moveDir = moveDir.normalized;

        Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = rotation;

        transform.Translate(moveDir * Time.deltaTime * moveSpeed, Space.World);        
    }
}
