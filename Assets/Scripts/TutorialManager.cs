using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    void Start()
    {
        tutorialPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        tutorialPanel.SetActive(false);

        Time.timeScale = 1f;

    }
}