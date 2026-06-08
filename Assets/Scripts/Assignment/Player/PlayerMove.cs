using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update() => Move();
    private void Move()
    {
        // Vector3 정규화
        Vector3 moveDir = new Vector3(
            playerInput.InputVector.x, 
            0, 
            playerInput.InputVector.y).normalized;        

        if (moveDir.magnitude > 0)
        {
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
            Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
