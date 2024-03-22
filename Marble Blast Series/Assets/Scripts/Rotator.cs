using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);    
    }
}
