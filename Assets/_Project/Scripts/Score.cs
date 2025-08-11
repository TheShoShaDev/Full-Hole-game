using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float _value;
    public float Value => _value;

    public void IncreseScore(float value)
    {
        _value = value; 
    }
}
