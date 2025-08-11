using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeRemaing;

    private void Update()
    {
        _timeRemaing -= Time.deltaTime;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(_timeRemaing / 60);
        int seconds = Mathf.FloorToInt(_timeRemaing % 60);

        /*if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }*/
    }

    private void TimerCompleted()
    {
        throw new System.NotImplementedException();
    }
}
