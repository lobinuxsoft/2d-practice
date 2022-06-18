using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameplayUI : MonoBehaviour
{
    UIDocument document;

    VisualElement transition;
    VisualElement resultPanel;
    Label resultLabel;
    Button resultButton;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        transition = document.rootVisualElement.Q<VisualElement>("transition");
        resultPanel = document.rootVisualElement.Q<VisualElement>("result-panel");
        resultLabel = document.rootVisualElement.Q<Label>("result-label");
        resultButton = document.rootVisualElement.Q<Button>("result-button");
        resultButton.clicked += ReturnToMainMenu;
    }

    private void Start()
    {
        HideResult();
        StartCoroutine(InitialShowScene(1));
    }

    private void OnDestroy() => resultButton.clicked -= ReturnToMainMenu;

    IEnumerator InitialShowScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        transition.SetEnabled(false);
    }

    private void HideResult() => resultPanel.SetEnabled(false);

    public void ShowResult(string text)
    {
        resultLabel.text = text;
        resultPanel.SetEnabled(true);
    }

    private void ReturnToMainMenu() => SceneManager.LoadScene("MainMenu");
}