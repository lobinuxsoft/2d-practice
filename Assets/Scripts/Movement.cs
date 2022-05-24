using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 10;

    Rigidbody2D body;
    Vector2 inputDir = Vector2.zero;

    private void Awake() => body = GetComponent<Rigidbody2D>();

    private void FixedUpdate() => body.velocity = inputDir * speed * Time.fixedDeltaTime;

    public void MoveInput(InputAction.CallbackContext context) => MoveDir(context.ReadValue<Vector2>());

    public void MoveDir(Vector2 dir) => inputDir = Vector2.ClampMagnitude(dir, 1);

#if UNITY_EDITOR
    [SerializeField] Color gizmosColor = Color.green;

    private void OnDrawGizmos()
    {
        if (TryGetComponent<Rigidbody2D>(out body))
        {
            Handles.color = gizmosColor;
            Handles.matrix = transform.localToWorldMatrix;
            Handles.CircleHandleCap(0, Vector3.zero, Quaternion.identity, 1, EventType.Repaint);
            Handles.ArrowHandleCap(0, Vector3.zero, Quaternion.LookRotation(body.velocity.normalized), Vector3.ClampMagnitude(body.velocity, 1).magnitude, EventType.Repaint);
        }
    }
#endif
}