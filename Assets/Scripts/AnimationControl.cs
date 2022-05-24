using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationControl : MonoBehaviour
{
    int horizontalHash = Animator.StringToHash("Horizontal");
    int verticalHash = Animator.StringToHash("Vertical");
    int speedHash = Animator.StringToHash("Speed");

    Animator animator;
    Rigidbody2D body;

    Vector3 dir = Vector3.zero;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if(body.velocity.magnitude > 0.1f)
        {
            dir = body.velocity.normalized;
        }

        animator.SetFloat(horizontalHash, dir.x);
        animator.SetFloat(verticalHash, dir.y);
        animator.SetFloat(speedHash, body.velocity.magnitude);
    }

}