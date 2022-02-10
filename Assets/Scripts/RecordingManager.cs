using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecordingManager : MonoBehaviour
{
    public List<ActorItem> cast;
    Controls controls;

    private void Awake()
    {
        controls = new Controls();

        controls.Recorder.Record.performed += _ => StartRecording(RecordingMode.Overwrite);
        controls.Recorder.Play.performed += _ => StartPlayback();
        //controls.Recorder.AdditiveRecord.performed += _ => StartRecording(RecordingMode.Additive);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        SetControl();
    }

    public void SetControl()
    {
        for(int i = 0; i < cast.Count; i++)
        {
            if(cast[i].actor != null)
                cast[i].actor.GetComponent<MovementController>().SetControllable(cast[i].movementEnabled);
        }
    }

    public void StartRecording(RecordingMode recordingMode)
    {
        for (int i = 0; i < cast.Count; i++)
        {
            if (cast[i].actor != null)
            {
                cast[i].actor.GetComponent<ActionRecorder>().StopRecording();
                cast[i].actor.GetComponent<ActionRecorder>().StopPlayback();

                if (cast[i].recordingEnabled)
                    cast[i].actor.GetComponent<ActionRecorder>().StartRecording(recordingMode);
                else
                    cast[i].actor.GetComponent<ActionRecorder>().StartPlayback();
            }
        }
    }

    public void StartPlayback()
    {
        for (int i = 0; i < cast.Count; i++)
        {
            if (cast[i].actor != null)
            {
                cast[i].actor.GetComponent<ActionRecorder>().StopRecording();
                cast[i].actor.GetComponent<ActionRecorder>().StopPlayback();

                cast[i].actor.GetComponent<ActionRecorder>().StartPlayback();
            }
        }
    }
}

[Serializable]
public class ActorItem
{
    public GameObject actor;
    public bool recordingEnabled;
    public bool movementEnabled;
    public bool specialEnabled;
}
