using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    float playerMaxSpeed;
    float playerJumpTime = 1.013593f;
    public CharacterController charControl;

    float nextSpawnDistance = 0;
    public GameObject platformPrefab;

    void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        playerMaxSpeed = charControl.GetMaxPlayerSpeed();

        // First platform
        nextSpawnDistance = 0;
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = nextSpawnDistance + (platformPrefab.transform.localScale.x / 2);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        nextSpawnDistance = (playerMaxSpeed * playerJumpTime) + (platformPrefab.transform.localScale.x);

        for (int i = 0; i < 20; i++)
        {
            // Create and transform new platform
            // This is just for testing. Final platforms will have set lengths
            // (Use 'GetComponent<Collider>().bounds.size' or 'GetComponent<Renderer>().bounds.size')
            newPlatform = Instantiate(platformPrefab);
            float platformScale = Random.Range(2f, 20f);
            newPlatform.transform.localScale = new Vector3(platformScale, 1, 1);

            // Set platform spawn position
            spawnPosition.x = nextSpawnDistance + (platformScale / 2);
            spawnPosition.y = Random.Range(-1f, 2f);
            newPlatform.transform.position = spawnPosition;

            // Move spawn distance
            nextSpawnDistance = spawnPosition.x + ((playerMaxSpeed * playerJumpTime) * Random.Range(0.25f, 1.08f)) + (platformScale / 2);
        }
    }
}
