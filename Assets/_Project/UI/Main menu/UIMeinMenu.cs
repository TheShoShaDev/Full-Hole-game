using UnityEngine;
using UnityEngine.UIElements;

public class ButtonHandler : MonoBehaviour
{
    // �������� UIDocument, � �������� �������� UI
    private UIDocument uiDocument;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();

        // ������� ������ �� ����� ��� ������
        Button myButton = uiDocument.rootVisualElement.Q<Button>("PlayButton");

        // ������������� �� ������� �����
        myButton.clicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        // ������������ ��� ���������� ������� (����� �������� ������ ������)
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