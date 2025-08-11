using UnityEngine;
using UnityEngine.UIElements;

public class ButtonHandler : MonoBehaviour
{
    // Получаем UIDocument, к которому привязан UI
    private UIDocument uiDocument;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();

        // Находим кнопку по имени или классу
        Button myButton = uiDocument.rootVisualElement.Q<Button>("PlayButton");

        // Подписываемся на событие клика
        myButton.clicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        // Отписываемся при выключении скрипта (чтобы избежать утечек памяти)
        if (uiDocument != null && uiDocument.rootVisualElement != null)
        {
            Button myButton = uiDocument.rootVisualElement.Q<Button>("PlayButton");
            myButton.clicked -= OnButtonClicked;
        }
    }

    private void OnButtonClicked()
    {
        SceneLoader sceneLoader = new SceneLoader();
        StartCoroutine(sceneLoader.LoadSceneAsync("Level1"));
    }
}