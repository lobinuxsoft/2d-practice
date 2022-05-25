using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10, maxAcceleration = 10, drag = 2;

    Rigidbody body;
    Vector2 inputDir = Vector2.zero;
    Vector3 velocity, desiredVelocity;

    private void Awake() 
    {
        body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotation;
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate() => MoveBehaviour();

    public void MoveInput(InputAction.CallbackContext context) => MoveDir(context.ReadValue<Vector2>());

    public void MoveDir(Vector2 dir) 
    {
        inputDir = Vector2.ClampMagnitude(dir, 1);

        Vector3 forward = Vector3.ProjectOnPlane(Vector3.forward, Vector3.up);
        Vector3 right = Vector3.ProjectOnPlane(Vector3.right, Vector3.up);

        desiredVelocity = (inputDir.x * right + inputDir.y * forward) * maxSpeed;

    }

    private void MoveBehaviour()
    {
        body.drag = drag;
        velocity = body.velocity;

        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity += desiredVelocity * maxSpeedChange;

        body.velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

#if UNITY_EDITOR
    [SerializeField] Color gizmosColor = Color.green;

    private void OnDrawGizmos()
    {
        if (TryGetComponent<Rigidbody>(out body))
        {
            Handles.color = gizmosColor;
            Handles.matrix = transform.localToWorldMatrix;
            Handles.CircleHandleCap(0, Vector3.zero, Quaternion.LookRotation(Vector3.up), 1, EventType.Repaint);
            Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.LookRotation(body.velocity.normalized), Vector3.ClampMagnitude(body.velocity, 1).magnitude, EventType.Repaint);
        }
    }
#endif
}