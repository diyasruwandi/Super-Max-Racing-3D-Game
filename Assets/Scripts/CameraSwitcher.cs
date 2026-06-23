using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject cockpitCamera;
    public GameObject freeLookCamera;

    private bool cockpitMode = false;

    public void SwitchCamera()
    {
        cockpitMode = !cockpitMode;

        if (cockpitMode)
        {
            cockpitCamera.SetActive(true);
            freeLookCamera.SetActive(false);
        }
        else
        {
            cockpitCamera.SetActive(false);
            freeLookCamera.SetActive(true);
        }
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
#endif
    }
}