using UnityEngine;
using TMPro;
using System.Collections;

public class RaceCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public mobil carController;

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // 🚫 mobil tidak bisa jalan dulu
        carController.enabled = false;

        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);

        // ✅ mobil bisa jalan
        carController.enabled = true;
        AICarController[] bots =
        FindObjectsByType<AICarController>(FindObjectsSortMode.None);
        
        foreach (AICarController bot in bots)
        {
            bot.canDrive = true;
        }
    }
}