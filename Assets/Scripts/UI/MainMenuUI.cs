using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] string nextSceneName = "";

    UIDocument document;

    // Main menu
    Button playButton;
    Button optionButton;
    Button creditsButton;
    Button quitButton;

    // Credit panel
    VisualElement creditPanel;
    Button closeCreditsButton;

    // Transition
    VisualElement transition;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        playButton = document.rootVisualElement.Q<Button>("play-button");
        optionButton = document.rootVisualElement.Q<Button>("option-button");
        creditsButton = document.rootVisualElement.Q<Button>("credits-button");
        quitButton = document.rootVisualElement.Q<Button>("quit-button");

        creditPanel = document.rootVisualElement.Q<VisualElement>("credits-panel");
        closeCreditsButton = document.rootVisualElement.Q<Button>("close-credits-button");

        transition = document.rootVisualElement.Q<VisualElement>("transition");

        playButton.clicked += PlayGame;
        optionButton.clicked += ShowOptions;
        creditsButton.clicked += ShowCredits;
        closeCreditsButton.clicked += HideCredits;
        quitButton.clicked += QuitGame;

        HideCredits();
    }

    private void Start()
    {
        StartCoroutine(InitialShowScene(1));
    }

    private void OnDestroy()
    {
        playButton.clicked -= PlayGame;
        optionButton.clicked -= ShowOptions;
        creditsButton.clicked -= ShowCredits;
        closeCreditsButton.clicked -= HideCredits;
        quitButton.clicked -= QuitGame;
    }

    private void PlayGame()
    {
        transition.RegisterCallback<TransitionEndEvent>(ChangeScene);
        transition.SetEnabled(true);
    }

    private void ShowOptions() => Debug.Log("OPCIONES!!!");

    private void ShowCredits() => creditPanel.SetEnabled(true);

    private void HideCredits() => creditPanel.SetEnabled(false);

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ChangeScene(TransitionEndEvent evt) => SceneManager.LoadScene(nextSceneName);

    IEnumerator InitialShowScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        transition.SetEnabled(false);
    }
}
