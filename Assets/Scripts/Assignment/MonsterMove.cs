using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private Transform target;

    private MonsterSight monsterSight;

    private float moveSpeed = 1f;

    private void Start()
    {
        monsterSight = GetComponent<MonsterSight>();
    }

    // Update is called once per frame
    void Update()
    {
        // #TODO: FSM 만들고 그에 따라 이동 여부 결정
        // if (monsterSight.IsSense)

        Move();
    }

    private void Move()
    {
        // Vector3 정규화
        Vector3 moveDir = target.position - transform.position.normalized;
        moveDir.y = 0;

        transform.position += moveDir * Time.deltaTime * moveSpeed;
        Quaternion rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = rotation;
    }
}
