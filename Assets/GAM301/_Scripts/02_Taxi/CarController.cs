using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    private float acceleration = 50f, deceleration = 30f, currentSpeed;

    [SerializeField] private PlayerScript player;

    // Settings
    [SerializeField] private float maxSpeed, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;



    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        if (player == null)
        {
            Debug.LogWarning("Khong tim thay player");
            return;
        }

        if (player.IsDriving() == true)
        {
            // Steering Input
            horizontalInput = Input.GetAxis("Horizontal");

            // Acceleration Input
            verticalInput = Input.GetAxis("Vertical");

            // Breaking Input
            isBreaking = Input.GetKey(KeyCode.Space);
        }
    }

    private void HandleMotor()
    {
        if (player.IsDriving() == true)
        {
            // Kiểm tra đầu vào tăng tốc
            if (Input.GetKey(KeyCode.W))
            {
                // Tăng tốc dần
                if (currentSpeed < maxSpeed)
                {
                    currentSpeed += acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                currentSpeed -= deceleration * Time.deltaTime;
                if(currentSpeed < 100f)
                {
                    currentSpeed = 0f;
                }
            }

            // Giới hạn tốc độ trong khoảng 0 đến maxSpeed
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

            // Áp dụng lực lên WheelCollider
            frontLeftWheelCollider.motorTorque = currentSpeed;
            frontRightWheelCollider.motorTorque = currentSpeed;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
            Debug.Log($"Current Speed: {currentSpeed}");
        }
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}