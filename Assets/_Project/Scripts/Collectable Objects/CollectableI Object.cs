using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class CollectableIObject : MonoBehaviour, ICollectable
{
    [SerializeField] private float _value;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _ico;

    [Inject] private Level _holeLevel;

    public static Action<int> OnCollected;
    public int Id => _id;
    public Sprite Ico => _ico;


    public void Collected()
    {
        IncreaceXP();
        HideObject();

        OnCollected?.Invoke(_id);
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    private void IncreaceXP()
    {
        _holeLevel.IncreaceXP(_value); 
    }
}
