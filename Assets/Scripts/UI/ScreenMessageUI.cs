using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenMessageUI : MonoBehaviour
{
    UIDocument document;

    VisualElement messagePanel;
    Label messageLabel;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        messagePanel = document.rootVisualElement.Q<VisualElement>("message-panel");
        messageLabel = document.rootVisualElement.Q<Label>("message-label");

        messagePanel.SetEnabled(false);
    }

    public void ShowMessage(string message)
    {
        messageLabel.text = message;
        messagePanel.RegisterCallback<TransitionEndEvent>(ShowingMessage);
        messagePanel.SetEnabled(true);
    }

    private void ShowingMessage(TransitionEndEvent evt) => StartCoroutine(HideRutine(1));

    IEnumerator HideRutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        messagePanel.SetEnabled(false);
    }
}
