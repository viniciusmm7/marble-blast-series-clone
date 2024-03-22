using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody playerRb;
    private Vector3 offset;
    public float sensitivity = 800.0f;
    private float rotationX = 20.0f;
    private float rotationY = 90.0f;
    private const float MinYAngle = -90.0f;
    private const float MaxYAngle = 90.0f;
    private const float DistanceToPlayer = 5.0f;
    private const float AutoRotationSpeed = 36.0f;
    private float mouseX;
    private float mouseY;

    private void SetupCamera()
    {
        offset = transform.position - player.transform.position;
        offset = offset.normalized * DistanceToPlayer;
    }

    private void Start()
    {
        SetupCamera();
    }

    private void FollowPlayer()
    {
        transform.position = player.transform.position + offset;
    }

    private void RotateCamera()
    {
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, MinYAngle, MaxYAngle);

        rotationY += mouseX;

        Transform transform1;
        (transform1 = transform).rotation = Quaternion.Euler(rotationX, rotationY, 0);
        
        transform1.position = player.transform.position - transform1.forward * offset.magnitude;
    }

    private void RotateForever()
    {
        rotationY += AutoRotationSpeed * Time.deltaTime;
        Transform transform1;
        (transform1 = transform).rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform1.position = player.transform.position - transform1.forward * offset.magnitude;
    }

    private void Update()
    {
        if (!playerRb.useGravity) return;
        FollowPlayer();
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!playerRb.useGravity)
        {
            RotateForever();
        }
    }

    private void LateUpdate()
    {
        if (playerRb.useGravity)
        {
            RotateCamera();
        }
    }
}
