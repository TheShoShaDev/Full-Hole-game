using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<CollectableIObject> _requaredCollected;

    private Dictionary<int, int> _collectableOnLevel = new Dictionary<int, int>(); //Id Count


    public Action<int> Collected;
    public int CollectiblesCount => _requaredCollected.Count;

    private void Awake()
    {
        if (_collectableOnLevel.Count == 0)
        {
            CountAllEnemies();
        }
    }

    private void HandleOnCollected(int id)
    {
       if (_collectableOnLevel.ContainsKey(id))
        {
            _collectableOnLevel[id]--;
            Collected?.Invoke(id);
            CheckWinCondition();
            Debug.Log("Collectables left: " + string.Join(", ", _collectableOnLevel));
        }

    }

    private void CheckWinCondition()
    {
        int clearedTypes = 0;

        foreach (KeyValuePair<int, int> keyValuePair in _collectableOnLevel)
        {
            if(keyValuePair.Value == 0)
            {
                clearedTypes++;
            }
        }

        if (clearedTypes == _requaredCollected.Count)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("🎉 Победа!");
    }

    private void CountAllEnemies()
    {
        _collectableOnLevel.Clear();

        CollectableIObject[] collectables = FindObjectsByType<CollectableIObject>(FindObjectsSortMode.None);

        HashSet<int> requiredIds = new HashSet<int>(_requaredCollected.Select(x => x.Id));

        CollectableIObject[] filteredCollectables = collectables
            .Where(obj => requiredIds.Contains(obj.Id))
            .ToArray();

        foreach (CollectableIObject collectable in filteredCollectables)
        {
            if (_collectableOnLevel.ContainsKey(collectable.Id))
            {
                _collectableOnLevel[collectable.Id]++;
            }
            else
            {
                _collectableOnLevel.Add(collectable.Id, 1);
            }
        }

        Debug.Log("Collectables counted: " + string.Join(", ", _collectableOnLevel));
    }

    public int GetCollectibleCountByIndex(int index)
    {
        if(_collectableOnLevel.Count == 0 )
        {
            CountAllEnemies();
        }
        return _collectableOnLevel.Values.ToList()[index];
    }

    public int GetCollectibleCountById(int id)
    {
        if (_collectableOnLevel.Count == 0)
        {
            CountAllEnemies();
        }
         
        if (_collectableOnLevel.TryGetValue(id, out int count))
        {
            return count;
        }
        else
        {
            throw new Exception("Value not found");
        }
    }

    public Sprite GetCollectibleIcon(int index)
    {
        return _requaredCollected[index].Ico;
    }

    public int GetRequaredIdByIndex(int index)
    {
        return _requaredCollected[index].Id;
    }

    private void OnEnable()
    {
        CollectableIObject.OnCollected += HandleOnCollected;
    }

    private void OnDisable()
    {
        CollectableIObject.OnCollected -= HandleOnCollected;
    }
}
