using UnityEngine;
using UnityEngine.InputSystem;

public class QuaternionInputGizmoPractice : MonoBehaviour
{
    public float rotationSpeed = 4f;
    public float targetMoveSpeed = 3f;
    public float targetDistance = 4f;
    public float targetRange = 3f;

    Vector3 targetOffset = new Vector3(0f, 0f, 4f);

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        Vector2 input = Vector2.zero;

        // aKey, leftArrowKey는 각각 A 키와 왼쪽 방향키를 나타내는 입력 버튼입니다.
        // isPressed는 해당 키가 지금 눌려 있는 동안 true가 되는 프로퍼티입니다.
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            input.x -= 1f;
        }

        // dKey, rightArrowKey는 각각 D 키와 오른쪽 방향키를 나타냅니다.
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            input.x += 1f;
        }

        // sKey, downArrowKey는 각각 S 키와 아래 방향키를 나타냅니다.
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            input.y -= 1f;
        }

        // wKey, upArrowKey는 각각 W 키와 위 방향키를 나타냅니다.
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            input.y += 1f;
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            targetOffset = new Vector3(0f, 0f, targetDistance);
        }

        targetOffset += new Vector3(input.x, input.y, 0f) * targetMoveSpeed * Time.deltaTime;

        targetOffset.x = Mathf.Clamp(targetOffset.x, -targetRange, targetRange);
        targetOffset.y = Mathf.Clamp(targetOffset.y, -targetRange, targetRange);
        targetOffset.z = targetDistance;

        Vector3 targetDirection = targetOffset.normalized;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        // transform.position은 현재 오브젝트의 월드 위치입니다.
        Vector3 origin = transform.position;
        Vector3 targetPosition = origin + targetOffset;
        Vector3 targetDirection = targetOffset.normalized;

        // Gizmos.color는 이후에 그릴 기즈모 도형의 색상을 지정하는 프로퍼티입니다.
        // Color.yellow는 유니티가 미리 제공하는 노란색 값입니다.
        Gizmos.color = Color.yellow;
        // Gizmos.DrawSphere는 Scene 뷰에 구체를 그려 특정 위치를 표시하는 메서드입니다.
        Gizmos.DrawSphere(targetPosition, 0.15f);

        // Color.blue는 유니티가 미리 제공하는 파란색 값입니다.
        Gizmos.color = Color.blue;
        // Gizmos.DrawLine은 Scene 뷰에 두 점을 잇는 선을 그리는 메서드입니다.
        // transform.forward는 현재 오브젝트가 바라보는 앞 방향입니다.
        Gizmos.DrawLine(origin, origin + transform.forward * targetDistance);

        // Color.red는 유니티가 미리 제공하는 빨간색 값입니다.
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + targetDirection * targetDistance);
    }
}
