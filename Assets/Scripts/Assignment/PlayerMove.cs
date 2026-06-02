using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private float moveSpeed = 5f;

    private Vector2 inputVector = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        // InputSystem
        if (Keyboard.current is not null) InputKeyboardValue(ref inputVector);

        Move();
    }

    private void InputKeyboardValue(ref Vector2 inputVector)
    {
        inputVector = Vector2.zero;

        float h = 0;
        float v = 0;

        // isPressed는 해당 키를 누르고 있는 동안 true가 되는 프로퍼티입니다.
        if (Keyboard.current.aKey.isPressed) h = -1;
        if (Keyboard.current.dKey.isPressed) h = 1;
        if (Keyboard.current.wKey.isPressed) v = 1;
        if (Keyboard.current.sKey.isPressed) v = -1;

        inputVector = new Vector2(h, v);
    }

    private void Move()
    {
        // Vector3 정규화
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y).normalized;        

        if (moveDir.magnitude > 0)
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
            Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
