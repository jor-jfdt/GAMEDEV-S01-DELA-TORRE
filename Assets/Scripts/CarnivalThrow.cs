using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CarnivalThrow : MonoBehaviour
{
    [Header("Throw Settings")]
    public GameObject ballObject;
    public Transform throwPoint;
    public float maxForce = 100f;
    public float chargeRate = 50f;

    [Header("UI")]
    public Slider powerSlider;

    [Header("Trajectory")]
    public LineRenderer lineRenderer;
    public int trajectorySteps = 30;
    public LayerMask ignoreLayers;

    private float currentForce = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isCharging = true;
            currentForce = 0f;
            if (lineRenderer != null) lineRenderer.enabled = true;
        }

        if (Mouse.current.leftButton.isPressed && isCharging)
        {
            currentForce += chargeRate * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0f, maxForce);

            if (lineRenderer != null) DrawTrajectory();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isCharging)
        {
            isCharging = false;
            ThrowBall();

            currentForce = 0f;
            if (lineRenderer != null) lineRenderer.enabled = false;
        }

        // Update Slider UI
        if (powerSlider != null)
        {
            powerSlider.value = currentForce / maxForce;
        }
    }

    void ThrowBall()
    {
        if (ballObject == null || throwPoint == null) return;

        GameObject thrownBall = Instantiate(ballObject, throwPoint.position, throwPoint.rotation);

        Rigidbody rb = thrownBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * currentForce, ForceMode.Impulse);
        }
    }

    void DrawTrajectory()
    {
        if (throwPoint == null || lineRenderer == null) return;

        Vector3 startPosition = throwPoint.position;
        Vector3 startVelocity = throwPoint.forward * currentForce;

        lineRenderer.positionCount = trajectorySteps;

        for (int i = 0; i < trajectorySteps; i++)
        {
            float t = i * 0.1f;

            // Kinematic position calculation: P = P0 + V0*t + 0.5*g*t^2
            Vector3 point = startPosition + (startVelocity * t);
            point.y = startPosition.y + (startVelocity.y * t) + (Physics.gravity.y / 2f * t * t);

            lineRenderer.SetPosition(i, point);
        }
    }
}