using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 cameraOffset;

    private Vector3 velocity = Vector3.zero;  // 속도 벡터 (SmoothDamp 내부용)
    public float smoothTime = 0.3f;           // 높을수록 더 느리게 따라감

    private void LateUpdate()
    {
        SmoothFollowing();
    }

    private void SmoothFollowing()
    {
        Vector3 targetPosition = target.position + cameraOffset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
}
