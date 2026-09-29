using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public enum TriggerAction
    {
        Rotate,
        ActivateMovement
    }

    [SerializeField]
    GameObject obj;
    [SerializeField]
    List<GameObject> allPlatforms = new();
    
    // Identifies what action to perform by the platform when stepped on by the switch
    [SerializeField]
    TriggerAction performAction;

    Vector3 pos;

    void Start()
    {
        pos = obj.transform.position;
    
        string filePath = Application.persistentDataPath + "/rotationdata.json";
        
        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            Quaternion savedRotation = JsonUtility.FromJson<Quaternion>(jsonContent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Finish")) {
                Debug.Log("Level Complete");
            }
            
            if (performAction == TriggerAction.Rotate) {
                obj.transform.Rotate(Vector3.up, 45);
            }
            else if (performAction == TriggerAction.ActivateMovement) {
                MovingPlatform pfScript = obj.GetComponent<MovingPlatform>();
                if (pfScript != null) {
                    pfScript.isMoving = true;
                }
                else {
                    Debug.LogWarning($"Trying to move {obj.name}, but it is missing the MovingPlatform script!");
                }
           }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (obj != null) {
            string data = JsonUtility.ToJson(obj.transform.rotation);
            File.WriteAllText(Application.persistentDataPath + "/rotationdata.json", data);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
