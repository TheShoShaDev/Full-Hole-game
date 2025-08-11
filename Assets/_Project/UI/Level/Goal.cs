using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Goal : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private LevelManager _levelManager;

    private Dictionary<int, Label> _countLabels = new Dictionary<int, Label>();
    private Dictionary<int, VisualElement> _visualElements = new Dictionary<int, VisualElement>();

    private VisualElement _matrixContainer;


    public void InitMatrix()
    {
        _matrixContainer.Clear();

        for (int i = 0; i < _levelManager.CollectiblesCount; i++)
        {
            if (_levelManager.GetCollectibleCountByIndex(i) <= 0) continue; // Пропускаем, если количество = 0

            VisualElement item = new VisualElement();
            item.AddToClassList("matrix-item");

            // Иконка
            var icon = new VisualElement();
            icon.AddToClassList("matrix-item-icon");
            icon.style.backgroundImage = new StyleBackground(_levelManager.GetCollectibleIcon(i));
            item.Add(icon);

            // Количество
            var countLabel = new Label(_levelManager.GetCollectibleCountByIndex(i).ToString());
            countLabel.AddToClassList("matrix-item-count");
            item.Add(countLabel);

            int id = _levelManager.GetRequaredIdByIndex(i);

            _countLabels.TryAdd(id, countLabel);
            _visualElements.TryAdd(id, item);

            _matrixContainer.Add(item);
        }
    }

    private void UpdateMatrix(int id)
    {
        _countLabels.TryGetValue(id, out Label countLabel);

        int count = _levelManager.GetCollectibleCountById(id);

        if (count > 0)
        {
            countLabel.text = count.ToString();
        }
        else
        {
            RemoveComponent(id);
        }
   
    }

    private void RemoveComponent(int id)
    {
        _visualElements.TryGetValue(id, out VisualElement visualElement);
        _matrixContainer.Remove(visualElement);
    }

    private void OnCollected(int id)
    {
        UpdateMatrix(id);
    }

    private void OnEnable()
    {
        _matrixContainer = _uiDocument.rootVisualElement.Q<VisualElement>("collectibleContainer");
        _levelManager.Collected += OnCollected;
        InitMatrix();
    }

    private void OnDisable()
    {
        _levelManager.Collected -= OnCollected;
    }

}
