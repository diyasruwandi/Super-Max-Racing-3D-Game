using UnityEngine;

public class mobil : MonoBehaviour
{
    [Header("Joystick")]
    public FixedJoystick joystick;

    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrakeForce;
    private bool isBraking;

    private Rigidbody rb;

    // SETTINGS
    [SerializeField] private float motorForce = 3000f;
    [SerializeField] private float brakeForce = 4000f;
    [SerializeField] private float maxSteerAngle = 25f;
    [SerializeField] private float downforce = 100f;
    [SerializeField] private float antiRollForce = 5000f;

    //AUDIO
    [SerializeField] private AudioSource engineSound;
    [SerializeField] private AudioSource idleAudio;
    [SerializeField] private AudioSource accelAudio;
    [SerializeField] private AudioSource decelAudio;

 
    // WHEEL COLLIDERS
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // WHEEL TRANSFORMS
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 🔥 Biar mobil ga gampang keangkat
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        ApplyDownforce();
        ApplyAntiRoll(frontLeftWheelCollider, frontRightWheelCollider);
        ApplyAntiRoll(rearLeftWheelCollider, rearRightWheelCollider);
        UpdateWheels();
        HandleEngineAudio();
        // HandleSpray();
    }

    private void GetInput()
    {
        // horizontalInput = Input.GetAxis("Horizontal");
        // verticalInput = Input.GetAxis("Vertical");
        // isBraking = Input.GetKey(KeyCode.Space);
        // horizontalInput = joystick.Horizontal;
        // verticalInput = joystick.Vertical;

        // isBraking = false;
         float keyboardH = Input.GetAxis("Horizontal");
         float keyboardV = Input.GetAxis("Vertical");
     
         float joystickH = joystick != null ? joystick.Horizontal : 0f;
         float joystickV = joystick != null ? joystick.Vertical : 0f;
     
         horizontalInput = keyboardH + joystickH;
         verticalInput = keyboardV + joystickV;
     
         horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
         verticalInput = Mathf.Clamp(verticalInput, -1f, 1f);
     
         isBraking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        // ✅ RWD (tenaga belakang)
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBrakeForce = isBraking ? brakeForce : 0f;
        ApplyBraking();
    }

    private void ApplyBraking()
    {
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        float speed = rb.linearVelocity.magnitude;

        // 🔥 Steering adaptif (biar ga liar saat kencang)
        float steerFactor = Mathf.Clamp01(speed / 50f);
        currentSteerAngle = maxSteerAngle * horizontalInput * (1f - steerFactor);

        // float steerReduction = Mathf.Clamp(speed / 100f, 0f, 0.5f);

        // currentSteerAngle = maxSteerAngle * horizontalInput * (1f - steerReduction);

        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    

    private void ApplyDownforce()
    {
        // 🔥 Biar mobil nempel ke tanah
        rb.AddForce(-transform.up * downforce * rb.linearVelocity.magnitude);
    }

    private void ApplyAntiRoll(WheelCollider leftWheel, WheelCollider rightWheel)
    {
        WheelHit hit;
        float travelLeft = 1.0f;
        float travelRight = 1.0f;

        bool groundedLeft = leftWheel.GetGroundHit(out hit);
        if (groundedLeft)
            travelLeft = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;

        bool groundedRight = rightWheel.GetGroundHit(out hit);
        if (groundedRight)
            travelRight = (-rightWheel.transform.InverseTransformPoint(hit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;

        float antiRoll = (travelLeft - travelRight) * antiRollForce;

        if (groundedLeft)
            rb.AddForceAtPosition(leftWheel.transform.up * -antiRoll, leftWheel.transform.position);

        if (groundedRight)
            rb.AddForceAtPosition(rightWheel.transform.up * antiRoll, rightWheel.transform.position);
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

   private void HandleEngineAudio()
{
    float speed = rb.linearVelocity.magnitude;

    bool accelerating = verticalInput > 0.1f;
    bool decelerating = verticalInput < 0.1f && speed > 5f;

    // 🔥 IDLE
    if (speed < 2f)
    {
        if (!idleAudio.isPlaying)
            idleAudio.Play();

        if (accelAudio.isPlaying)
            accelAudio.Stop();

        if (decelAudio.isPlaying)
            decelAudio.Stop();
    }

    // 🔥 ACCELERATION
    else if (accelerating)
    {
        if (!accelAudio.isPlaying)
            accelAudio.Play();

        if (idleAudio.isPlaying)
            idleAudio.Stop();

        if (decelAudio.isPlaying)
            decelAudio.Stop();

        accelAudio.pitch = Mathf.Lerp(1f, 1.3f, speed / 100f);    
    }

    // 🔥 DECELERATION / ENGINE BRAKE
    else if (decelerating)
    {
        if (!decelAudio.isPlaying)
            decelAudio.Play();

        if (idleAudio.isPlaying)
            idleAudio.Stop();

        if (accelAudio.isPlaying)
            accelAudio.Stop();

       decelAudio.pitch = Mathf.Lerp(1f, 1.2f, speed / 100f);
    }
}

//     private void HandleSpray()
// {
//     float speed = rb.linearVelocity.magnitude;

//     if (speed > 5f)
//     {
//         PlaySpray(rearLeftSpray, speed * 5f);
//         PlaySpray(rearRightSpray, speed * 5f);

//         // 🔥 depan lebih kecil
//         PlaySpray(frontLeftSpray, speed * 4f);
//         PlaySpray(frontRightSpray, speed * 4f);
//     }
//     else
//     {
//         StopSpray(rearLeftSpray);
//         StopSpray(rearRightSpray);

//         StopSpray(frontLeftSpray);
//         StopSpray(frontRightSpray);
//     }
// }

// private void PlaySpray(ParticleSystem spray, float rate)
// {
//     if (!spray.isPlaying)
//         spray.Play();

//     var emission = spray.emission;
//     emission.rateOverTime = rate;
// }

// private void StopSpray(ParticleSystem spray)
// {
//     if (spray.isPlaying)
//         spray.Stop();
// }
}