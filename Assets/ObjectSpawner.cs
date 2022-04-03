using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    int listCounter = 0;

    public Transform positionToSpawn;

    float throwForce = 4f;
    GameObject previousObject;
    float previousHorizontalDirection = 0.5f;

    private void Start()
    {
        Random.InitState(42);
    }

    public void SpawnNextItem()
    {
        if (previousObject != null)
        {
            Vector2 throwDirection = NewThrowDirection();
            Rigidbody2D prevRB = previousObject.GetComponent<Rigidbody2D>();
            prevRB.bodyType = RigidbodyType2D.Dynamic;
            prevRB.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
        }

        if (listCounter < objectsToSpawn.Count)
        {
            previousObject = Instantiate(objectsToSpawn[listCounter], positionToSpawn);
            listCounter++;
        }
    }

    Vector2 NewThrowDirection()
    {
        Vector2 finalThrowDirection = Vector2.zero;
        finalThrowDirection.x = previousHorizontalDirection + (Random.Range(-0.9f, 0.5f) * previousHorizontalDirection);

        finalThrowDirection.y = 1.5f + Random.Range(-0.5f, 0.5f);

        previousHorizontalDirection *= -1f;
        return finalThrowDirection;
    }
}
