using UnityEngine;

public class AICarController : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    [Header("Car Settings")]
    public float motorTorque = 1500f;
    public float maxSteerAngle = 25f;
    public float maxSpeed = 80f;

    [Header("Downforce")]
    public float downforce = 100f;

    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    public bool canDrive = false;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    private void FixedUpdate()
    {
        if (!canDrive)
            return;
            
        Drive();
        Steer();
        ApplyDownforce();
        CheckWaypointDistance();
        UpdateWheels();
    }

 private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheel, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheel, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheel, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheel, rearRightWheelTransform);
    }

     private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }



     private void Drive()
{
//     Transform targetWaypoint = waypoints[currentWaypoint];

//     Vector3 localTarget = transform.InverseTransformPoint(targetWaypoint.position);

//     // 🔥 Deteksi tikungan
//     float turnAmount = Mathf.Abs(localTarget.x / localTarget.magnitude);

//     // 🔥 Torque berbeda
//     float currentTorque;

//     // Kalau tikungan
//     if (turnAmount > 0.1f)
//     {
//         currentTorque = 200f;
//     }
//     else
//     {
//         currentTorque = 900f;
//     }

//     // Kasih tenaga ke roda belakang
//     rearLeftWheel.motorTorque = currentTorque;
//     rearRightWheel.motorTorque = currentTorque;

Transform targetWaypoint = waypoints[currentWaypoint];

Vector3 localTarget =
transform.InverseTransformPoint(targetWaypoint.position);

float turnAmount =
Mathf.Abs(localTarget.x / localTarget.magnitude);

float currentTorque;

// 🔥 Hairpin / tikungan tajam
if (turnAmount > 0.2f)
{
    currentTorque = 100f;

}

// 🔥 Tikungan biasa
else if (turnAmount > 0.1f)
{
    currentTorque = 100f;

}

// 🔥 Jalan lurus
else
{
    currentTorque = 900f;

}

rearLeftWheel.motorTorque = currentTorque;
rearRightWheel.motorTorque = currentTorque;
}

    private void Steer()
    {
        Transform targetWaypoint = waypoints[currentWaypoint];

        Vector3 localTarget = transform.InverseTransformPoint(targetWaypoint.position);

        float steer = (localTarget.x / localTarget.magnitude) * maxSteerAngle;

        frontLeftWheel.steerAngle = steer;
        frontRightWheel.steerAngle = steer;
    }

    private void CheckWaypointDistance()
    {
        float distance = Vector3.Distance(
            transform.position,
            waypoints[currentWaypoint].position
        );

        if (distance < 8f)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }

    private void ApplyDownforce()
    {
        rb.AddForce(
            -transform.up * downforce * rb.linearVelocity.magnitude
        );
    }

}