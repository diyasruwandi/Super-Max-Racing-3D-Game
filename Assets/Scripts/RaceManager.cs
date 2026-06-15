using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public LapCounter playerLapCounter;
    public LapCounter aiLapCounter;

    public FinishManager finishManager;

    private bool raceEnded = false;

    void Update()
    {
        if (raceEnded)
            return;

        // PLAYER MENANG
        if (playerLapCounter.raceFinished)
        {
            raceEnded = true;

            finishManager.FinishRace(true);
        }

        // AI MENANG
        if (aiLapCounter.raceFinished)
        {
            raceEnded = true;

            finishManager.FinishRace(false);
        }
    }
}