using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public Transform player;
    public GameObject playerGameObject;
    private const float X = -25.0f;
    private const float Y = 0.5f;
    private const float Z = 0.0f;

    private AudioManager audioManager;
    
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void ResetPlayer()
    {
        player.position = new Vector3(X, Y, Z);
        playerGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerGameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        audioManager.PlaySfx(audioManager.outOfBounds);
        ResetPlayer();
    }
}
