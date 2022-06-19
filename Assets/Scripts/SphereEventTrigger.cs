#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class SphereEventTrigger : MonoBehaviour
{
    public UnityEvent<GameObject> onTriggerEnterEvent;

    private SphereCollider sphere;

    private void Awake()
    {
        sphere = GetComponent<SphereCollider>();
        sphere.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnterEvent?.Invoke(other.gameObject);
    }

#if UNITY_EDITOR
    public Color gizmoColor = Color.green;
    private GUIStyle labelStyle;
    private void OnDrawGizmos()
    {
        if (labelStyle == null)
        {
            labelStyle = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Game).label;
            labelStyle.richText = true;
            labelStyle.alignment = TextAnchor.MiddleCenter;
        }

        if (!sphere) sphere = GetComponent<SphereCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(sphere.center, sphere.radius);
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, .25f);
        Gizmos.DrawSphere(sphere.center, sphere.radius);
        string text = $"<color=#{ColorUtility.ToHtmlStringRGB(gizmoColor)}><b>{gameObject.name}</b></color>";
        Handles.matrix = transform.localToWorldMatrix;
        Handles.Label(sphere.center, text, labelStyle);
    }
#endif
}