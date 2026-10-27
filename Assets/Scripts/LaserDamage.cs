using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public int damage = 25; // Adjust damage as needed

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}