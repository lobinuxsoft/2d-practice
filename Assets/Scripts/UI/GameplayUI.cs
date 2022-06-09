using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI : MonoBehaviour
{
    UIDocument document;

    VisualElement transition;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        transition = document.rootVisualElement.Q<VisualElement>("transition");
    }

    private void Start()
    {
        StartCoroutine(InitialShowScene(1));
    }

    IEnumerator InitialShowScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        transition.SetEnabled(false);
    }
}