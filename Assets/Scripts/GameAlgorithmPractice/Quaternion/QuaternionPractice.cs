using UnityEngine;

public class QuaternionPractice : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform drawStartPoint;

    public float rotationSpeed = 4f;
    public float targetMoveSpeed = 3f;
    public float targetDistance = 4f;
    public float targetRange = 3f;

    // Update is called once per frame
    void Update()
    {
        // 타겟의 위
        Vector3 targetDirection = (target.position - transform.position).normalized;

        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {
        // transform.position은 현재 오브젝트의 월드 위치입니다.
        Vector3 origin = drawStartPoint.position;
        Vector3 targetDirection = (target.position - transform.position).normalized;

        Vector3 targetDirection2 = targetDirection;
        targetDirection2.y = 0;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(origin, origin + targetDirection2 * targetDistance );

        // Color.red는 유니티가 미리 제공하는 빨간색 값입니다.
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, target.position);
    }
}
