using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class ManualGravity : MonoBehaviour
{
    private float _gravity = -10f;
    private float impactRange = 0f;
    private float moveSpeed = 3f;
    private Vector3 distance;

    [SerializeField] private float currentVelocityY = 0f;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        distance = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
        // 타겟과의 수평 방향 거리 구하기
        Vector3 direction = distance.normalized;

        // 중력 가속도
        currentVelocityY += _gravity * Time.deltaTime;

        // 떨어지는 물체가 타겟 방향으로 날라가도록 방향과 속도를 알려줌
        Vector3 pos = transform.position + (direction * moveSpeed * Time.deltaTime);

        // 중력가속도 적용으로 내 위치가 땅으로 떨어지게
        pos.y += currentVelocityY * Time.deltaTime;

        if (pos.y < 0)
        {
            ShockWave(currentVelocityY);
            pos.y = 0;
            currentVelocityY = 0;
        }

        transform.position = pos;
    }

    private void ShockWave(float velocity)
    {
        if (velocity < 0) velocity *= -1f;
        impactRange = velocity * 10f;
        Debug.Log($"충격파 범위 : {impactRange}");
    }
}
