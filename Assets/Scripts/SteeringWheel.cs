using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public float maxSteeringWheelRotation = 200f;

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        // ambil steer angle roda
        float steer =
            frontLeftWheel.steerAngle;

        // ubah jadi rotasi setir
        float rotation =
            (steer / 30f) *
            maxSteeringWheelRotation;

        // rotasi setir
        transform.localRotation =
            startRotation *
            Quaternion.Euler(0, rotation, 0);
    }
}