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
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif

        Debug.Log("Game Closed");
    }
}