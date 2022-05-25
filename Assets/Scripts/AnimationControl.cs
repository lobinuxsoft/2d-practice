using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationControl : MonoBehaviour
{
    int horizontalHash = Animator.StringToHash("Horizontal");
    int verticalHash = Animator.StringToHash("Vertical");
    int speedHash = Animator.StringToHash("Speed");

    Animator animator;
    Rigidbody body;

    Vector3 dir = Vector3.zero;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if(body.velocity.magnitude > 0.1f)
        {
            dir = body.velocity.normalized;
        }

        animator.SetFloat(horizontalHash, dir.x);
        animator.SetFloat(verticalHash, dir.z);
        animator.SetFloat(speedHash, body.velocity.magnitude);
    }
}