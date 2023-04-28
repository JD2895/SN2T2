using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followTarget;
    Vector3 newPosition = Vector3.zero;

    public Vector3 followOffset;

    void Start()
    {
        newPosition = followTarget.position + followOffset;
        this.transform.position = newPosition;
    }

    void Update()
    {
        // Only follow in the x direction (+offset). Ignore y.
        newPosition.x = followTarget.position.x + followOffset.x;
        this.transform.position = newPosition;
    }
}
