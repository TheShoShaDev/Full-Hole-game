using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private uint _currentLevel = 1;
    [SerializeField] private float _currentXP;
    [SerializeField] private float _requredXP;
    [SerializeField] private float _xpRate = 0.3f;
    [SerializeField] private float _baseGrowthRate = 0.11f;    
    [SerializeField] private float _growthExponent = 2.2f;     
    [SerializeField] private float _dampingFactor = 0.65f;    
    public uint CurrentLevel => _currentLevel;


    public void IncreaceXP(float value)
    {
        _currentXP =+ value;

        if(_currentXP >= _requredXP)
        {
            _currentXP = 0;
            Upgrade();
            CalculateRequaredXP();
            _currentLevel += 1;
        }
    }

    private void Upgrade()
    {
        Vector3 curScale = this.gameObject.transform.localScale;

        float newScale = GetLevelScale(curScale.x);
        float xRate = newScale / curScale.x;

        gameObject.transform.localScale = new Vector3(newScale, curScale.y, newScale);

        Vector3 cameraPos = _camera.transform.position;

        _camera.transform.position = new Vector3(cameraPos.x, cameraPos.y * xRate, cameraPos.z);
    }

    private void CalculateRequaredXP()
    {
        _requredXP *= 1 + _xpRate * _currentLevel;
    }

    private float GetLevelScale(float curScale)
    {
        return curScale + (_baseGrowthRate * _currentLevel * _growthExponent) / (1 + _dampingFactor * Mathf.Log(_currentLevel + 1));
    }

}
