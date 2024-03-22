using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private Collider endPlatformCollider;
    private AudioManager audioManager;

    private const float Speed = 20.0f;
    private const float JumpForce = 7.0f;
    // private const float MaxAngularVelocity = 10.0f;
    
    private bool isGrounded;
    private bool isGameActive = true;
	private bool isWinConditionMet;
    private int count;
    private float movementX;
    private float movementY;
    
    public int countWinCondition;
    public float timeRemaining = 30.0f;
    
    public Text countText;
    public Text timerText;
    public Text winConditionText;
    public GameObject winMenu;
    public GameObject gameOverCanvas;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ShowMessage(countText, count + "/" + countWinCondition);
        winConditionText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        winMenu.SetActive(false);
        gameOverCanvas.SetActive(false);
        mainCamera = Camera.main;
        Time.timeScale = 1;
    }

    private void ShowMessage(Text textObject, string message, float duration = -1.0f, bool isTutorial = false)
    {
        textObject.gameObject.SetActive(true);
        textObject.text = message;
		if (isTutorial) audioManager.PlaySfx(audioManager.helpTrigger);
        if (duration > 0.0f) StartCoroutine(HideMessageAfterDelay(textObject, duration));
    }

    private static IEnumerator HideMessageAfterDelay(Component textObject, float duration)
    {
        yield return new WaitForSeconds(duration);
        textObject.gameObject.SetActive(false);
    }

    private void Jump()
    {
        if (!isGrounded || !isGameActive) return;
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        audioManager.PlaySfx(audioManager.playerJump);
    }

    private void GameOver()
    {
        gameOverCanvas.SetActive(true);
        ShowMessage(timerText, "Time's up!");
        audioManager.StopRollingSfx();
        Time.timeScale = 0;
    }

    private void UpdateTime()
    {
        if (!isGameActive) return;
        timeRemaining -= Time.deltaTime;
        var timeRemainingInt = (int) timeRemaining;
        ShowMessage(timerText, "Time Remaining: " + timeRemainingInt);
    }
    
    private void Update()
    {
        UpdateTime();
        if (timeRemaining <= 0) GameOver();
        if (Input.GetButtonDown("Jump")) Jump();
    }

    private Vector3 GetCameraForward()
    {
        var cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        return cameraForward;
    }

    private Vector3 GetCameraRight()
    {
        var cameraRight = mainCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        return cameraRight;
    }

    private Vector3 GetMovementDirection()
    {
        if (!isGameActive) return Vector3.zero;

        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var cameraForward = GetCameraForward();
        var cameraRight = GetCameraRight();

        var forwardRelative = cameraForward * moveVertical;
        var rightRelative = cameraRight * moveHorizontal;

        var movementDirection = forwardRelative + rightRelative;

        return movementDirection;
    }

    // private void RotateToMove()
    // {
    //     var movementDirection = GetMovementDirection();
    //     var angularSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f) * MaxAngularVelocity;
    //     var forward = GetCameraForward() * movementDirection.z;
    //     var right = GetCameraRight() * movementDirection.x;
    //     rb.angularVelocity = angularSpeed * (forward + right);
    // }

    private void RelativeMove()
    {
        var movementDirection = GetMovementDirection();
    
        movementX = movementDirection.x;
        movementY = movementDirection.z;
    
        if (movementDirection.magnitude > 1)
        {
            movementDirection.Normalize();
        }
    }
    
    private void NormalizeSpeed()
    {
        if (rb.velocity.magnitude > Speed)
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }
    }
    
    private void MovePlayer(string type)
    {
        var movement = new Vector3(movementX, 0.0f, movementY);
        switch (type)
        {
            case "grounded":
                rb.AddForce(movement * Speed);
                break;
            case "airborne":
                rb.AddForce(movement * Speed / 5);
                break;
        }
    }
    private void SmoothLockPlayer()
    {
        rb.useGravity = false;
        
        var platformCenter = endPlatformCollider.bounds.center;
        var targetPosition = new Vector3(platformCenter.x, platformCenter.y + 2.0f, platformCenter.z);
        
        var playerPosition = transform.position;

        var gravityDirection = (targetPosition - playerPosition).normalized;

        var gravityForce = gravityDirection * rb.mass;
        gravityForce *= 100.0f;

        rb.AddForce(gravityForce);

        var newPosition = Vector3.Lerp(playerPosition, targetPosition, Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
        
        if (Vector3.Distance(playerPosition, targetPosition) < 0.3f)
        {
            transform.position = targetPosition;
        }
    }

    private void FixedUpdate()
    {
        if (isGameActive)
        {
            // RotateToMove();
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

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground") || isGrounded) return;
        isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground") || !isGrounded) return;
        isGrounded = false;
        audioManager.StopRollingSfx();
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        isGrounded = true;
        if (isGrounded && rb.velocity.magnitude > 1.0f)
        {
            audioManager.PlaySfx(audioManager.rolling, true);
            return;
        }
        audioManager.StopRollingSfx();
    }

    private void CollectPickUp(Component pickUp)
    {
        pickUp.gameObject.SetActive(false);
        count++;
        ShowMessage(countText, count + "/" + countWinCondition);
		isWinConditionMet = count >= countWinCondition;
        audioManager.PlaySfx(audioManager.gemCollected);
        if (!isWinConditionMet) return;
        audioManager.PlaySfx(audioManager.allGemsCollected);
        ShowMessage(winConditionText, "You collected all the pick ups!", 4.0f);
    }

    private void EndGame(Collider other)
    {
        isGameActive = false;
        endPlatformCollider = other;
        winMenu.SetActive(true);
        audioManager.PlaySfx(audioManager.finishLevel);
    }

    private void TutorialMessages(Component other)
    {
        if (other.gameObject.CompareTag("MoveTutorial"))
        {
            ShowMessage(winConditionText, "Use WASD or Arrow Keys to move!", 4.0f, true);
        }

        if (other.gameObject.CompareTag("CollectTutorial"))
        {
            ShowMessage(winConditionText, "Collect all the pick ups to win!", 4.0f, true);
        }

        if (other.gameObject.CompareTag("JumpTutorial"))
        {
            ShowMessage(winConditionText, "Press Space to jump!", 4.0f, true);
        }

        if (other.gameObject.CompareTag("TimeTutorial"))
        {
            ShowMessage(winConditionText, "You have 30 seconds to complete the level!", 4.0f, true);
        }

        if (other.gameObject.CompareTag("CameraTutorial"))
        {
            ShowMessage(winConditionText, "Move the mouse to look around!", 4.0f, true);
        }
        
        if (other.gameObject.CompareTag("RestartTutorial"))
        {
            ShowMessage(winConditionText, "Press T to restart the level!", 4.0f, true);
        }

        if (other.gameObject.CompareTag("PauseTutorial"))
        {
            ShowMessage(winConditionText, "Press Esc to pause the game!", 4.0f, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            CollectPickUp(other);
        }
        
        TutorialMessages(other);
            
        if (!other.gameObject.CompareTag("EndPlatform")) return;
        if (isWinConditionMet)
        {
            EndGame(other);
        }
        else
        {
            audioManager.PlaySfx(audioManager.missingGems);
            ShowMessage(winConditionText, "You need to collect all the pick ups!", 4.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BoundsTutorial"))
        {
            ShowMessage(winConditionText, "Stay within the bounds!", 4.0f, true);
        }
    }
}
