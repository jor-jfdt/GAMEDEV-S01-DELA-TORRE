using UnityEngine;
using System.Collections.Generic;

public class LaserManager : MonoBehaviour
{
    [Header("Laser Setup")]
    public GameObject[] laserPatterns;
    public float moveSpeed = 5f;
    public float spawnInterval = 2f;

    [Header("Waypoints")]
    public Transform spawnPoint;
    public Transform endPoint;

    private float spawnTimer;
    private List<GameObject> activePatterns = new List<GameObject>();

    void Start()
    {
        // Hide all patterns at start (if they are in the scene)
        foreach (GameObject pattern in laserPatterns)
        {
            pattern.SetActive(false);
        }

        SpawnRandomPattern();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            SpawnRandomPattern();
            spawnTimer = spawnInterval;
        }

        for (int i = activePatterns.Count - 1; i >= 0; i--)
        {
            GameObject pattern = activePatterns[i];

            pattern.transform.position = Vector3.MoveTowards(
                pattern.transform.position,
                endPoint.position,
                moveSpeed * Time.deltaTime
            );

            if (Vector3.Distance(pattern.transform.position, endPoint.position) < 0.1f)
            {
                activePatterns.RemoveAt(i);
                Destroy(pattern);
            }
        }
    }

    void SpawnRandomPattern()
    {
        if (laserPatterns.Length == 0) return;

        int randomIndex = Random.Range(0, laserPatterns.Length);

        // Include spawnPoint.position
        GameObject newPattern = Instantiate(laserPatterns[randomIndex], spawnPoint.position, spawnPoint.rotation);

        // Ensure the new clone is active
        newPattern.SetActive(true);

        activePatterns.Add(newPattern);
    }
}