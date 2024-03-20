using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBound : MonoBehaviour
{
    public Transform player;
    public GameObject playerGameObject;
    private float x = -25.0f;
    private float y = 0.5f;
    private float z = 0.0f;
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.position = new Vector3(x, y, z);
        }
    }
}
