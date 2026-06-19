using UnityEngine;


[RequireComponent(typeof(Animator))]
public class SimpleAnimatorDriver : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeedAnim(float speed)
    {
        animator.SetFloat(SpeedHash, speed, 0.1f, Time.deltaTime);
    }
}
