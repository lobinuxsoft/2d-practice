using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] Vector3 posOffset = Vector3.up;
    [SerializeField] float speed = 10;

    Vector3 destiny = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
        float step = speed * Time.deltaTime;
        destiny = followTarget.position + posOffset;
        transform.position = Vector3.SlerpUnclamped(transform.position, destiny, step);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(followTarget.position + posOffset, .25f);
        Gizmos.DrawLine(transform.position, followTarget.position + posOffset);
    }
#endif
}
