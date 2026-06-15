using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject cockpitCamera;

    public GameObject freeLookCamera;

    private bool cockpitMode = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cockpitMode = !cockpitMode;

            // ================= COCKPIT =================
            if (cockpitMode)
            {
                cockpitCamera.SetActive(true);

                freeLookCamera.SetActive(false);
            }

            // ================= THIRD PERSON =================
            else
            {
                cockpitCamera.SetActive(false);

                freeLookCamera.SetActive(true);
            }
        }
    }
} 