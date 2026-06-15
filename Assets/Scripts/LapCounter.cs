using UnityEngine;
using TMPro;

public class LapCounter : MonoBehaviour
{
    public TextMeshProUGUI lapText;

    public int currentLap = 0;
    public int maxLap = 3;

    public bool raceFinished = false;

    private void Start()
    {
        UpdateLapUI();
    }

    public void AddLap()
    {
        if (raceFinished)
            return;

        currentLap++;

        if (currentLap > maxLap)
        {
            raceFinished = true;

            Debug.Log(gameObject.name + " FINISH!");
        }

        UpdateLapUI();
    }

    void UpdateLapUI()
    {
        if (lapText != null)
        {
            lapText.text =
                "Lap " + currentLap + "/" + maxLap;
        }
    }
}