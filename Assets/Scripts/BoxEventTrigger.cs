#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class BoxEventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> onTriggerEnterEvent;

    private BoxCollider box;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        box.isTrigger = true;
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

        if (!box) box = GetComponent<BoxCollider>();

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(box.center, box.size);
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, .25f);
        Gizmos.DrawCube(box.center, box.size);
        string text = $"<color=#{ColorUtility.ToHtmlStringRGB(gizmoColor)}><b>{gameObject.name}</b></color>";
        Handles.matrix = transform.localToWorldMatrix;
        Handles.Label(box.center, text, labelStyle);
    }
#endif
}