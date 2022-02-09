using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecordingManager : MonoBehaviour
{
    public List<ActorItem> cast;
}

[Serializable]
public class ActorItem
{
    public GameObject actor;
    public bool recordingEnabled;
    public bool movementEnabled;
    public bool specialEnabled;
}
