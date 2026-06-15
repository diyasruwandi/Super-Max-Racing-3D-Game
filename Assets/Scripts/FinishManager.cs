using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class FinishManager : MonoBehaviour
{
    public GameObject finishPanel;

    public TMP_Text resultText;

    public Image winnerImage;
    public Image flagWinnerImage;
    public Image nameWinnerImage;

    public Sprite playerWinSprite;
    public GameObject playerWinVideoObject;

    public VideoPlayer videoPlayer;
    public AudioSource engineAudio;

    public VideoClip playerWinVideo;
    public Sprite aiWinSprite;
    public Sprite flagPlayerWinSprite;
    public Sprite flagAiWinSprite;
    public Sprite namePlayerWinSprite;
    public Sprite nameAiWinSprite;

    public mobil playerCarController;


    public void FinishRace(bool playerWin)
    {
        finishPanel.SetActive(true);

        if (playerWin)
        {
            // resultText.text = "POSITION : P1";
        
            // sembunyikan gambar winner lama
            winnerImage.gameObject.SetActive(false);
            flagWinnerImage.gameObject.SetActive(false);
            nameWinnerImage.gameObject.SetActive(false);
        
            // tampilkan video
            playerWinVideoObject.SetActive(true);
              // MATIKAN KONTROL MOBIL
            playerCarController.enabled = false;
            engineAudio.Stop();
        
            // play video
            videoPlayer.clip = playerWinVideo;
            videoPlayer.Play();
        }
        else
        {
            resultText.text = "POSITION : P1";
        
            // tampilkan gambar AI
            winnerImage.gameObject.SetActive(true);
            flagWinnerImage.gameObject.SetActive(true);
            nameWinnerImage.gameObject.SetActive(true);
        
            // sembunyikan video
            playerWinVideoObject.SetActive(false);
        
            winnerImage.sprite = aiWinSprite;
            flagWinnerImage.sprite = flagAiWinSprite;
            nameWinnerImage.sprite = nameAiWinSprite;
        }

        Time.timeScale = 0f;
    }

    public void RestartRace()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}