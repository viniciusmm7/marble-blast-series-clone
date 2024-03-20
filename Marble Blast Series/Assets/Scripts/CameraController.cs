using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Rigidbody playerRb;
    private Vector3 offset;
    public float sensitivity = 800.0f;
    private float rotationX = 20.0f;
    private float rotationY = 90.0f;
    private float minYAngle = -90.0f;
    private float maxYAngle = 90.0f;
    private float distanceToPlayer = 5.0f;
    private float autoRotationSpeed = 36.0f;
    private float mouseX;
    private float mouseY;

    private void SetupCamera()
    {
        offset = transform.position - player.transform.position;
        offset = offset.normalized * distanceToPlayer;
    }

    void Start()
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
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);

        rotationY += mouseX;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        
        transform.position = player.transform.position - transform.forward * offset.magnitude;
    }

    private void RotateForever()
    {
        rotationY += autoRotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.position = player.transform.position - transform.forward * offset.magnitude;
    }

    private void Update()
    {
        if (playerRb.useGravity)
        {
            FollowPlayer();
            mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        }
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
