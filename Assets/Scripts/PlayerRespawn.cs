using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    public float fallThreshold = -10f;

    void Update() 
    {
        if (transform.position.y < fallThreshold) {
            Respawn();
        }
    }

    private void Respawn() 
    {
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null) {
            cc.enabled = false;
        }

        transform.position = spawnPoint.position;

        if (cc != null) {
            cc.enabled = true;
        }
    }
}