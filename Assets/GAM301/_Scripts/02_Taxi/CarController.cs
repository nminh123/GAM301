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
        if (!player.IsDriving())
        {
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
            ApplyBreaking(); // Đảm bảo xe dừng lại
            return;
        }

        // Kiểm tra đầu vào tăng tốc hoặc lùi
        if (Input.GetKey(KeyCode.W))
        {
            // Tăng tốc tới
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Chạy lùi
            if (currentSpeed > -maxSpeed / 2) // Giới hạn tốc độ lùi là 50% maxSpeed
            {
                currentSpeed -= acceleration * Time.deltaTime;
            }
        }
        else
        {
            // Tự động giảm tốc độ nếu không nhấn W hoặc S
            if (currentSpeed > 0f)
            {
                currentSpeed -= deceleration * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0f); // Đảm bảo không giảm dưới 0 khi xe đang tiến
            }
            else if (currentSpeed < 0f)
            {
                currentSpeed += deceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, 0f); // Đảm bảo không tăng quá 0 khi xe đang lùi
            }
        }

        // Áp dụng giới hạn tốc độ
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2, maxSpeed);

        // Áp dụng lực lên WheelCollider
        frontLeftWheelCollider.motorTorque = currentSpeed;
        frontRightWheelCollider.motorTorque = currentSpeed;

        // Áp dụng lực phanh
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();

        Debug.Log($"Current Speed: {currentSpeed}");
    }

    private void ApplyBreaking()
    {
        if (!player.IsDriving())
        {
            currentbreakForce = breakForce; // Áp dụng lực phanh tối đa
        }

        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        if (!player.IsDriving())
        {
            // Đặt góc lái về 0 khi không lái
            frontLeftWheelCollider.steerAngle = 0f;
            frontRightWheelCollider.steerAngle = 0f;
            return;
        }

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