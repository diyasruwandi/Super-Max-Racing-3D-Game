using UnityEngine;

public class StartLineTriggerRace : MonoBehaviour
{
    public LapTimer lapTimer;
    public LapCounter lapCounter;
    private float aiLastTriggerTime = -10f;

    private bool hasStarted = false;

    private bool playerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb == null)
            return;

        // ================= PLAYER =================
        if (rb.CompareTag("Player") && !playerInside)
        {
            playerInside = true;

            LapTimer playerTimer =
                other.GetComponentInParent<LapTimer>();

            LapCounter playerLapCounter =
                other.GetComponentInParent<LapCounter>();


            if (!hasStarted)
            {
                if (playerTimer != null)
                    playerTimer.StartLap();

                if (playerLapCounter != null)
                    playerLapCounter.AddLap();

                hasStarted = true;
            }
            else
            {
                if (playerTimer != null)
                {
                    playerTimer.EndLap();

                    playerTimer.StartLap();
                }

                if (playerLapCounter != null)
                    playerLapCounter.AddLap();
            }

            Debug.Log("PLAYER LEWAT START");
        }

    //     if (rb.CompareTag("Player") && !playerInside)
    //   {
    //            playerInside = true;
           
    //            if (!hasStarted)
    //            {
    //                lapTimer.StartLap();
           
    //                lapCounter.AddLap();
           
    //                hasStarted = true;
    //            }
    //            else
    //            {
    //                lapTimer.EndLap();
           
    //                lapTimer.StartLap();
           
    //                lapCounter.AddLap();
    //            }
           
    //            Debug.Log("PLAYER LEWAT START");
    //  }           

        // ================= AI =================
        if (rb.CompareTag("AI"))
        {
            if (Time.time - aiLastTriggerTime > 2f)
            {
                aiLastTriggerTime = Time.time;

                LapCounter aiLapCounter =
                    rb.GetComponent<LapCounter>();

                if (aiLapCounter != null)
                {
                    aiLapCounter.AddLap();
                }

                Debug.Log("AI LEWAT START LINE");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb == null)
            return;

        if (rb.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}