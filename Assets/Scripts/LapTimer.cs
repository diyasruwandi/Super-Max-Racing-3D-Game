using UnityEngine;
using TMPro;

public class LapTimer : MonoBehaviour
{
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI bestTimeText;

    private float currentTime;
    private float bestTime = Mathf.Infinity;

    private bool isTiming = false;

    void Update()
    {
        if (isTiming)
        {
            currentTime += Time.deltaTime;

            if (currentTimeText != null)
            {
            currentTimeText.text = "Current: " + FormatTime(currentTime);
            }
        }
    }

    public void StartLap()
    {
        currentTime = 0f;
        isTiming = true;
    }

    public void EndLap() // ✅ INI YANG KURANG
    {
        isTiming = false;

        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            bestTimeText.text = "Best: " + FormatTime(bestTime);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}