using UnityEngine;

public class SimpleCollision : MonoBehaviour
{
    public Transform other;
    public float radiusA = 1.0f;
    public float radiusB = 1.0f;

    private bool IsOverlapping()
    {
        if (other == null)
        {
            return false;
        }

        Vector3 diff = transform.position - other.position;

        float distanceSq = diff.sqrMagnitude;

        float radiusSum = radiusA + radiusB;
        float radiusSumSq = radiusSum * radiusSum;

        return distanceSq <= radiusSumSq;
    }

    private void OnDrawGizmos()
    {
        // OnDrawGizmos는 Scene 뷰에 개발용 시각 표시를 그릴 때 사용하는 Unity 메시지 메서드입니다.
        if (other == null)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, radiusA);
            return;
        }

        bool isOverlapping = IsOverlapping();

        // 충돌하지 않으면 초록색, 충돌하면 빨간색으로 범위를 표시합니다.
        Gizmos.color = isOverlapping ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusA);
        Gizmos.DrawWireSphere(other.position, radiusB);

        // 두 중심점 사이의 거리를 눈으로 확인할 수 있도록 선을 그립니다.
        Gizmos.DrawLine(transform.position, other.position);
    }
}
