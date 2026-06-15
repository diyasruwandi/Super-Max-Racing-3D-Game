using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public Light rearLight;

    public float blinkSpeed = 0.2f;

    private bool lightOn = true;

    private void Start()
    {
        InvokeRepeating(nameof(BlinkLight), 0f, blinkSpeed);
    }

    private void BlinkLight()
    {
        lightOn = !lightOn;
        rearLight.enabled = lightOn;
    }
}