using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastJump : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 5f;
    public float checkDistance = .6f;

    void Start() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, checkDistance);

        Debug.DrawRay(transform.position, Vector3.down * checkDistance, isGrounded ? Color.green : Color.red);

        if (isGrounded && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
