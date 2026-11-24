using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveAxis = Vector3.right;
    public float moveDistance = 3f;
    public float moveSpeed = 2.0f;

    [Header("Hit Behavior")]
    public bool dropWhenHit = false;

    private Vector3 startPosition;
    private bool isKnockedDown = false;
    private Rigidbody rb;
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isKnockedDown) return;

        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPosition + (moveAxis.normalized * offset);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (dropWhenHit && !isKnockedDown && collision.rigidbody != null)
        {
            isKnockedDown = true;

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
        }
    }
}
