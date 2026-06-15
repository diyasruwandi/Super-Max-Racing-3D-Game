using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadDryTrack()
    {
        SceneManager.LoadScene("DryTrackScene");
    }

    public void LoadWetTrack()
    {
        SceneManager.LoadScene("WetTrackScene");
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Game Closed");
    }
}