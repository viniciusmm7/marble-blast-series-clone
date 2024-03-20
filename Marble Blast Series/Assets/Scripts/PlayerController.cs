using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private Collider endPlatformCollider;

    private float speed = 20.0f;
    private float jumpForce = 7.0f;
    private bool isGrounded;
    private bool isGameActive = true;
    private int count;
    public int countWinCondition = 0;
    private float movementX;
    private float movementY;
    
    public TextMeshProUGUI countText;
    public TextMeshProUGUI winConditionText;
    public GameObject winConditionTextObject;
    public GameObject winMenu;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        ShowMessage(countText, "Count: " + count.ToString());
        winConditionTextObject.SetActive(false);
        winMenu.SetActive(false);
        mainCamera = Camera.main;
    }

    private void ShowMessage(TextMeshProUGUI textObject, string message, float duration = -1.0f)
    {
        textObject.gameObject.SetActive(true);
        textObject.text = message;

        if (duration > 0.0f)
        {
            StartCoroutine(HideMessageAfterDelay(textObject, duration));
        }
    }

    private IEnumerator HideMessageAfterDelay(TextMeshProUGUI textObject, float duration)
    {
        yield return new WaitForSeconds(duration);
        textObject.gameObject.SetActive(false);
    }

    private void Jump()
    {
        if (isGrounded && isGameActive)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private Vector3 GetCameraForward()
    {
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        return cameraForward;
    }

    private Vector3 GetCameraRight()
    {
        Vector3 cameraRight = mainCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        return cameraRight;
    }

    private Vector3 GetMovementDirection()
    {
        if (!isGameActive)
        {
            return Vector3.zero;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = GetCameraForward();
        Vector3 cameraRight = GetCameraRight();

        Vector3 forwardRelative = cameraForward * moveVertical;
        Vector3 rightRelative = cameraRight * moveHorizontal;

        Vector3 movementDirection = forwardRelative + rightRelative;

        return movementDirection;
    }

    private void RelativeMove()
    {
        Vector3 movementDirection = GetMovementDirection();

        movementX = movementDirection.x;
        movementY = movementDirection.z;

        if (movementDirection.magnitude > 1)
        {
            movementDirection.Normalize();
        }
    }

    private void NormalizeSpeed()
    {
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void MovePlayer(string type)
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        if (type == "grounded") rb.AddForce(movement * speed);
        if (type == "airborne") rb.AddForce(movement * speed / 5);
    }
    private void SmoothLockPlayer()
    {
        rb.useGravity = false;
        
        Vector3 platformCenter = endPlatformCollider.bounds.center;
        Vector3 targetPosition = new Vector3(platformCenter.x, platformCenter.y + 2.0f, platformCenter.z);

        Vector3 gravityDirection = (targetPosition - transform.position).normalized;

        Vector3 gravityForce = gravityDirection * 100.0f * rb.mass;

        rb.AddForce(gravityForce);

        Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
        {
            transform.position = targetPosition;
        }
    }

    private void FixedUpdate()
    {
        if (isGameActive)
        {
            RelativeMove();
            if (isGrounded)
            {
                NormalizeSpeed();
                MovePlayer("grounded");
            }
            else
            {
                MovePlayer("airborne");
            }
        }
        else
        {
            SmoothLockPlayer();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") && isGrounded)
        {
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void CollectPickUp(Collider pickUp)
    {
        pickUp.gameObject.SetActive(false);
        count++;
        ShowMessage(countText, "Count: " + count.ToString());
    }

    private void EndGame(Collider collider)
    {
        isGameActive = false;
        endPlatformCollider = collider;
        winMenu.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            CollectPickUp(other);
        }

        if (other.gameObject.CompareTag("EndPlatform"))
        {
            if (count >= countWinCondition)
            {
                EndGame(other);
            }
            else
            {
                ShowMessage(winConditionText, "You need to collect all the pick ups!", 4.0f);
            }
        }
    }
}
