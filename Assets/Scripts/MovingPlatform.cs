using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Tooltip("Set the movement axis (e.g., X=1 for Right, Y=1 for Up, Z=1 for Forward)")]
    [SerializeField] 
    private Vector3 direction = new Vector3(1, 0, 0); 

    [SerializeField] 
    private float speed = 3f;

    public bool isMoving = false;

    void Update() 
    {
        if (isMoving)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlatformBoundary"))
        {
            direction = -direction;
        }
    }
}
